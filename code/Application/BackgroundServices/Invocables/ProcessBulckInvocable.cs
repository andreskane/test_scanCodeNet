
using Application.Interfaces.Documental;
using Application.Interfaces.Repositories;
using Coravel.Invocable;
using Domain.Entities;
using Domain.Entities.DynamicFormAggregate;
using Domain.Entities.Layout;
using Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Application.BackgroundServices.Invocables
{
    public class ProcessBulckInvocable : IInvocable
    {
        private readonly IBulkRepository _bulkRepository;
        private readonly IBulckComponentRepository _bulckComponentRepository;
        private readonly IDynamicFormRepository _dynamicFormRepository;
        private readonly IDynamicFormItemRepository _dynamicFormItemRepository;
        private readonly IDocDynamicFormRepository _docDynamicFormRepository;
        private readonly ILogger<ProcessBulckInvocable> _logger;

        public ProcessBulckInvocable(
            IBulkRepository bulkRepository,
            IDynamicFormRepository dynamicFormRepository,
            IDynamicFormItemRepository dynamicFormItemRepository,
            IDocDynamicFormRepository docDynamicFormRepository,
             IBulckComponentRepository bulckComponentRepository,
            ILogger<ProcessBulckInvocable> logger)
        {
            _logger = logger;
            _bulkRepository = bulkRepository;
            _bulckComponentRepository = bulckComponentRepository;
            _dynamicFormRepository = dynamicFormRepository;
            _dynamicFormItemRepository = dynamicFormItemRepository;
            _docDynamicFormRepository = docDynamicFormRepository;

        }

        public async Task Invoke()
        {
            await ProcessBulckTask(new CancellationToken(false));
        }

        private async Task ProcessBulckTask(CancellationToken cancelationToken)
        {
            _logger.LogInformation("START BACKGROUNDSERVICE");

            var ListBulk = await _bulkRepository.GetBulkProcessesAsyncByStatus(ProcessStatusEnum.ReadyToProcess, cancelationToken);

            if (ListBulk.Count > 0)
            {

                foreach (var itemBulkProcess in ListBulk)
                {


                    try
                    {


                        itemBulkProcess.Status = ProcessStatusEnum.InProgress;
                        await _bulkRepository.UpdateAsync(itemBulkProcess, cancelationToken);

                        var listComponentsToAdd = await _bulckComponentRepository.GetBulkComponentByBulckId(itemBulkProcess.Id, cancelationToken);
                        if (listComponentsToAdd.Count == 0)
                        {
                            itemBulkProcess.Status = ProcessStatusEnum.Completed;
                            await _bulkRepository.UpdateAsync(itemBulkProcess, cancelationToken);
                            continue;
                        }

                        var CantRowsToAdd = listComponentsToAdd.Count();

                        var ListDynamicFormItems = await _dynamicFormItemRepository.GetDynamicFormItemsByBulkId(itemBulkProcess.Id, cancelationToken);
                        if (ListDynamicFormItems.Count == 0)
                        {
                            itemBulkProcess.Status = ProcessStatusEnum.Completed;
                            await _bulkRepository.UpdateAsync(itemBulkProcess, cancelationToken);
                            continue;
                        }

                        foreach (var oDynamicFormItem in ListDynamicFormItems)
                        {
                            switch (itemBulkProcess.ProcessType)
                            {
                                case (ProcessTypeEnum.NewElement):

                                    processDynamicFormNewComponent(oDynamicFormItem, listComponentsToAdd, itemBulkProcess, CantRowsToAdd);
                                    break;
                                case (ProcessTypeEnum.EditElement):
                                    processDynamicFormEditComponent(oDynamicFormItem, listComponentsToAdd, itemBulkProcess, CantRowsToAdd);
                                    break;

                            }

                        }

                        itemBulkProcess.Status = ProcessStatusEnum.Completed;
                        await _bulkRepository.UpdateAsync(itemBulkProcess, cancelationToken);


                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        itemBulkProcess.Status = ProcessStatusEnum.Failed;
                        await _bulkRepository.UpdateAsync(itemBulkProcess, cancelationToken);
                    }




                }
            }

            _logger.LogInformation("END BACKGROUNDSERVICE");
        }
        private async void processDynamicFormEditComponent(DynamicFormItem oDynamicFormItem, IList<BulckComponent> listComponentsToEdit, BulkProcess itemBulkProcess, Int32 CantRowsToAdd)
        {
            var layout = await _docDynamicFormRepository.GetDynamicFormByKey(oDynamicFormItem.CodeFlow);


            for (int i = 0; i < layout.Pages.Count; i++)
            {
                var oListWorkflowItem = layout.Pages[i].workflowTable;
                List<WorkflowItem> newListWorkflowItem = new List<WorkflowItem>();
                foreach (var component in listComponentsToEdit)
                {
                    foreach (var item in oListWorkflowItem)
                    {
                        if (item.name == component.Name)
                        {
                            newListWorkflowItem.Add(MapEditComponent(item, component));
                        }
                        else
                        {
                            newListWorkflowItem.Add(item);
                        }
                    }
                }
                layout.Pages[i].workflowTable = newListWorkflowItem;
                i++;

            }

            await _docDynamicFormRepository.UpdateDynamicForm(new RootObject() { Pages = layout.Pages }, oDynamicFormItem.CodeFlow);



        }

        private async void processDynamicFormNewComponent_OLD(DynamicFormItem oDynamicFormItem, IList<BulckComponent> listComponentsToAdd, BulkProcess itemBulkProcess, Int32 CantRowsToAdd)
        {
            var layout = await _docDynamicFormRepository.GetDynamicFormByKey(oDynamicFormItem.CodeFlow);

            Int32 CantCols = 0;
            Int32 CantRows = 0;
            Int32 CantPages = 0;

            Int32 SearchCols = 0;
            Int32 SearchRows = 0;
            Int32 SearchPages = 0;
            Boolean ComponentFound = false;

            if (layout != null)
            {
                CantCols = 8;
                CantRows = layout.Pages[0].workflowTable.Count / 8;
                CantPages = layout.Pages.Count;
            }





            var p = 0;


            var oListWorkflowItem = layout.Pages[p].workflowTable;
            WorkflowItem[,] wItems = ConvertListToMatrix(oListWorkflowItem, CantRows, CantCols);

            String SearchComponent = "";
            if (itemBulkProcess.ComponentOfReference != null)
            {
                SearchComponent = itemBulkProcess.ComponentOfReference;
            }

            if (itemBulkProcess.ComponentOfReference != "")
            {
                for (int i = 0; i < CantCols; i++)
                {
                    for (int j = 0; j < CantRows; j++)
                    {
                        if (wItems[j, i].name == SearchComponent)
                        {
                            SearchCols = i;
                            SearchRows = j;
                            SearchPages = layout.Pages[i].page;
                            ComponentFound = true;
                        }
                        if (ComponentFound)
                        {
                            break;
                        }
                    }
                    if (ComponentFound)
                    {
                        break;
                    }
                }
            }


            int LineToInsert = 0;
            switch (itemBulkProcess.PlacementPreference)
            {
                case (PlacementPreferenceEnum.StartForm):
                    LineToInsert = 0;
                    break;
                case (PlacementPreferenceEnum.EndForm):
                    LineToInsert = CantRows + CantRowsToAdd - 1;
                    break;
                case (PlacementPreferenceEnum.PreviusComponent):
                    if (ComponentFound)
                    {
                        LineToInsert = SearchRows;
                    }
                    else
                    {
                        LineToInsert = 0;
                    }
                    break;
                case (PlacementPreferenceEnum.FollowingComponent):
                    if (ComponentFound)
                    {
                        LineToInsert = SearchRows + 1;
                    }
                    else
                    {
                        LineToInsert = CantRows + CantRowsToAdd - 1;
                    }
                    break;
            }


            
            wItems = ConvertMatrix(wItems, CantRows + CantRowsToAdd, LineToInsert);
            foreach (var component in listComponentsToAdd)
            {
                wItems[LineToInsert, 0] = MapNewComponent(component);
                LineToInsert++;
            }


            layout.Pages[p].workflowTable = ConvertMatrixToList(wItems);




            await _docDynamicFormRepository.UpdateDynamicForm(new RootObject() { Pages = layout.Pages }, oDynamicFormItem.CodeFlow);



        }

        private async void processDynamicFormNewComponent(DynamicFormItem oDynamicFormItem, IList<BulckComponent> listComponentsToAdd, BulkProcess itemBulkProcess, Int32 CantRowsToAdd)
        {

            var layout = await _docDynamicFormRepository.GetDynamicFormByKey(oDynamicFormItem.CodeFlow);

            Int32 CantCols = 0;
            Int32 CantRows = 0;
            Int32 CantPages = 0;

            Int32 SearchCols = 0;
            Int32 SearchRows = 0;
            Int32 SearchPages = 0;
            Boolean ComponentFound = false;

            if (layout != null)
            {
                CantCols = 8;
                CantRows = layout.Pages[0].workflowTable.Count / 8;
                CantPages = layout.Pages.Count;
            }


          
            String SearchComponent = "";
            if (itemBulkProcess.ComponentOfReference != null)
            {
                SearchComponent = itemBulkProcess.ComponentOfReference;
                for (var iPages = 0; iPages < CantPages; iPages++)
                {
                    var oListWorkflowItem = layout.Pages[iPages].workflowTable;
                    WorkflowItem[,] wItems = ConvertListToMatrix(oListWorkflowItem, CantRows, CantCols);
                    if (itemBulkProcess.ComponentOfReference != "")
                    {
                        for (int i = 0; i < CantCols; i++)
                        {
                            for (int j = 0; j < CantRows; j++)
                            {
                                if (wItems[j, i].name == SearchComponent)
                                {
                                    SearchCols = i;
                                    SearchRows = j;
                                    SearchPages = layout.Pages[i].page;
                                    ComponentFound = true;
                                }
                                if (ComponentFound)
                                {
                                    break;
                                }
                            }
                            if (ComponentFound)
                            {
                                break;
                            }
                        }
                    }

                }
            }

    
             if (itemBulkProcess.PlacementPreference == PlacementPreferenceEnum.StartForm || itemBulkProcess.PlacementPreference == PlacementPreferenceEnum.EndForm)
            {
                SearchPages = 0;
            }
 
            for (var iPages = 0; iPages < CantPages; iPages++)
            {
                if (iPages == SearchPages)
                {
                    var oListWorkflowItem = layout.Pages[iPages].workflowTable;
                    WorkflowItem[,] wItems = ConvertListToMatrix(oListWorkflowItem, CantRows, CantCols);

                    int LineToInsert = 0;
                    switch (itemBulkProcess.PlacementPreference)
                    {
                        case (PlacementPreferenceEnum.StartForm):
                            LineToInsert = 0;
                            break;
                        case (PlacementPreferenceEnum.EndForm):
                            LineToInsert = CantRows + CantRowsToAdd - 1;
                            break;
                        case (PlacementPreferenceEnum.PreviusComponent):
                            if (ComponentFound)
                            {
                                LineToInsert = SearchRows;
                            }
                            else
                            {
                                LineToInsert = 0;
                            }
                            break;
                        case (PlacementPreferenceEnum.FollowingComponent):
                            if (ComponentFound)
                            {
                                LineToInsert = SearchRows + 1;
                            }
                            else
                            {
                                LineToInsert = CantRows + CantRowsToAdd - 1;
                            }
                            break;
                    }

                    wItems = ConvertMatrix(wItems, CantRows + CantRowsToAdd, LineToInsert);
                    foreach (var component in listComponentsToAdd)
                    {
                        wItems[LineToInsert, 0] = MapNewComponent(component);
                        LineToInsert++;
                    }

                    layout.Pages[iPages].workflowTable = ConvertMatrixToList(wItems);

                }
            }
            await _docDynamicFormRepository.UpdateDynamicForm(new RootObject() { Pages = layout.Pages }, oDynamicFormItem.CodeFlow);
        }


        private static List<WorkflowItem> ConvertMatrixToList(WorkflowItem[,] matrix)
        {
            List<WorkflowItem> listDynamicFlowItems = new List<WorkflowItem>();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    listDynamicFlowItems.Add(matrix[i, j]);
                }
            }

            return listDynamicFlowItems;
        }

        private static WorkflowItem[,] ConvertListToMatrix(List<WorkflowItem> lista, int rows, int columns)
        {
            WorkflowItem[,] matrix = new WorkflowItem[rows, columns];

            for (int i = 0; i < lista.Count; i++)
            {
                int row = i / columns;
                int column = i % columns;
                matrix[row, column] = lista[i];
            }

            return matrix;
        }

        private static WorkflowItem[,] ConvertMatrix(WorkflowItem[,] originalMatrix, int newRows, int insertIndex)
        {
            int originalRows = originalMatrix.GetLength(0);
            int columns = originalMatrix.GetLength(1);
            if (insertIndex < 0 || insertIndex > originalRows)
            {
                Console.WriteLine("Error: El índice de inserción está fuera de los límites.");
                return null;
            }
            WorkflowItem[,] newMatrix = new WorkflowItem[newRows, columns];

            for (int i = 0; i < insertIndex; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    newMatrix[i, j] = originalMatrix[i, j];
                }
            }
            for (int j = 0; j < columns; j++)
            {
                newMatrix[insertIndex, j] = new WorkflowItem()
                {
                    properties = new Properties()
                    {
                        selectedRule = new SelectedRule()
                    }
                };
            }
            for (int i = insertIndex + 1; i < newRows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    newMatrix[i, j] = originalMatrix[(i - 1) % originalRows, j];
                }
            }
            return newMatrix;
        }
        private static WorkflowItem MapNewComponent(BulckComponent component)
        {


            var response = new WorkflowItem()
            {



                reordering = false,
                empty = false,
                name = component.Name,
                type = component.typeComponent.ToString(),
                icon = "",
                uniqueName = "",
                component = "",
                colSpanValue = 0,
                rowSpanValue = 0,
                isHidden = false,
                properties = new Properties()
                {
                    description = component.Description,
                    inputId = component.InputId,
                    label = component.Label,
                    value = component.Value,
                    isHidden = component.IsHidden,
                    required = component.Required,
                    src = "",
                    alt = "",
                    maxHeight = "",
                    maxWidth = "",
                    color = "",
                    fontSize = "",
                    fontWeight = "",
                    placeholder = "",
                    disabled = false,
                    selectedRule = new SelectedRule()
                    {
                        id = "",
                        description = "",
                        value = "",
                    },
                }

            };

            return response;
        }



        private static WorkflowItem MapEditComponent(WorkflowItem SourceComponent, BulckComponent dataToChange)
        {

      
            SourceComponent.properties.description = dataToChange.Description.ToString();
            SourceComponent.properties.label = dataToChange.Label;
            SourceComponent.properties.value = dataToChange.Value;
            SourceComponent.properties.isHidden = dataToChange.IsHidden;
            SourceComponent.properties.required = dataToChange.Required;


            return SourceComponent;
        }

    }
}
