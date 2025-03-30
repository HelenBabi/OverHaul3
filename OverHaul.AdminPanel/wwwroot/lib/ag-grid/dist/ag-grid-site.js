const AG_GRID_DEFAULT_OPTIONS =
{
    headerHeight: 25,
    floatingFiltersHeight: 26,
    enableRtl: true,
    tooltipShowDelay: 100,
    tooltipMouseTrack: true,
    defaultColDef:
    {
        sortable: true,
        floatingFilter: true,
        filter: 'agTextColumnFilter',
        resizable: true
      
    },
    rowSelection:
    {

        mode: 'singleRow',
        enableClickSelection: true,
        checkboxes: false
    },
    suppressPaginationPanel: true,
    suppressColumnVirtualisation: true,
    defaultExcelExportParams: {
        sheetName: 'Sheetl'
    },
    excelStyles: [{
        id: 'rstringType',
        dataType: 'string'
    }],
    //onFilterChanged: agGridAutoSizeColumnWidthOnFilterChanged
}

const AG_GRID_DEFAULT_OPTIONS_WITH_CHECKBOX = {
    headerHeight: 25,
    floatingFiltersHeight: 26,
    enableRtl: true,
    tooltipShowDelay: 100,
    tooltipMouseTrack: true,
    defaultColDef:
        { sortable: true, floatingFilter:true, filter: 'agTextColumnFilter', resizable: true },
    rowSelection: {
        mode: 'multiRow',
        enableClickSelection: false,
        checkboxes: true,
        headerCheckbox: true,
        selectAll: "filtered",
        groupSelects: "filteredDescendants",
        copySelectedRows: false,
        hideDisabledCheckboxes: false
    },
    suppressPaginationPanel: true,
    suppressColumnVirtualisation: true,
    defaultExcelExportParams: {
        sheetName: 'Sheetl1'
    },
    excelStyles: [{
        id: 'stringType',
        dataType: 'string'
    }],
    //onFilterChanged: agGridAutoSizeColumnWidthOnFilterChanged
}


