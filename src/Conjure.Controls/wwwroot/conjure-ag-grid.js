window.gridMap_getRows = {};
window.createGrid = function (gridRef, gridId, gridOptions) {
    gridOptions.rowModelType = 'infinite';
    gridOptions.datasource = {
        destroy: function () {
            console.log("destroying  datasource...");
        },
        getRows: function (getRowsParams) {
            console.log("getting rows:");
            console.log(getRowsParams);
            gridMap_getRows[gridId] = getRowsParams;
            gridRef.invokeMethodAsync('GetRows',
                getRowsParams.startRow,
                getRowsParams.endRow,
                getRowsParams.sortModel,
                JSON.stringify(getRowsParams.filterModel))
                .then(r => {
                    console.log("Got GetRows Result: ");
                    console.log(r);
                    if (r.success) {
                        getRowsParams.successCallback(r.blockRows, r.lastRow);
                    }
                    else {
                        getRowsParams.failCallback();
                    }

                });
        }
    };

    gridOptions.rowSelection = 'single';
    gridOptions.rowDeselection = true;
    gridOptions.suppressRowClickSelection = false;
    gridOptions.onSelectionChanged = function (ev) {
        var sel = ev.api.getSelectedRows();
        console.log("Selected Rows:");
        console.log(sel);
        gridRef.invokeMethodAsync("SelectionChanged", JSON.stringify(sel));
    };

    console.log("Creating Grid from: " + gridId + " with options: ");
    console.log(gridOptions);

    var gridDiv = document.getElementById(gridId);
    new agGrid.Grid(gridDiv, gridOptions);
};
