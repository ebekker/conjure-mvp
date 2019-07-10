window.gridMap_getRows = {};
window.gridMap_api = {};
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
    window.gridMap_api[gridId] = gridOptions.api;
};
window.gridDispose = function (gridId) {
    console.log("scheduling disposing grid stuff: " + gridId);
    // Since this is called in Blazor component IDisposable.Dispose()
    // we can't await this, so for now we run it a separate timer so
    // that we can return immediately...
    this.setTimeout(function () {
        console.log("disposing grid stuff: " + gridId);
        if (window.gridMap_getRows.hasOwnProperty(gridId)) {
            console.log("deleteing gridMap.getRows");
            delete window.gridMap_getRows[gridId];
        }
        if (window.gridMap_api.hasOwnProperty(gridId)) {
            console.log("deleteing gridMap.api");
            delete window.gridMap_api[gridId];
        }
    }, 100);
}
window.dumpSelection = function (gridId) {
    var api = window.gridMap_api[gridId];
    console.log("Got API: ");
    console.log(api);
    console.log("Dumping Selection: ");
    console.log(api.getSelectedRows());
    console.log(api.getSelectedNodes());

    var sel = api.getSelectedRows();
    if (sel && sel.length) {
        sel[0].temperatureC++;
    }
};
window.updateGridSelection = function (gridId, updateSel) {
    var api = window.gridMap_api[gridId];
    var sel = api.getSelectedNodes();

    console.log("BEFORE:");
    console.log(sel);
    if (sel && sel.length) {
        sel[0].setData(JSON.parse(updateSel)[0]);
    }
    console.log("AFTER:");
    console.log(sel);
};
