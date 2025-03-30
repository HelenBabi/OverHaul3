const AG_GRID_DEFAULT_OPTIONS = {
    headerHeight: 25,
    floatingFiltersHeight: 26,
    enableRtl: true,
    tooltipShowDelay: 100,
    tooltipMouseTrack: true,
    defaultColDef: {
        sortable: true,
        floatingFilter: true,
        filter: 'agTextColumnFilter',
        resizable: true,
        cellClass: "text-center",
        suppressSizeToFit: true,
    },
    rowSelection: {
        mode: 'singleRow',
        enableClickSelection: true,
        checkboxes: false
    },
    enableCellTextSelection: true,
    pagination: true,
    paginationAutoPageSize: true, // اندازه خودکار صفحه
    localeText: AG_GRID_LOCALE_FA,
    suppressColumnVirtualisation: true,
    defaultExcelExportParams: {
        sheetName: 'Sheet1'
    },
    excelStyles: [
        {
            id: 'stringType',
            dataType: 'string'
        }
    ],
    //onFilterChanged: agGridAutoSizeColumnWidthOnFilterChanged
}
const AG_GRID_DEFAULT_OPTIONS_WITH_CHECKBOX = {
    headerHeight: 25,
    floatingFiltersHeight: 26,
    enableRtl: true,
    tooltipShowDelay: 100,
    tooltipMouseTrack: true,
    defaultColDef: { sortable: true, floatingFilter: true, filter: 'agTextColumnFilter', resizable: true },
    rowSelection: {
        mode: 'multiRow',
        enableClickSelection: false,
        checkboxes: true,
        headerCheckbox: true,
        selectAll: "filtered",
        groupSelects: "filteredDescendants",
        copySelectedRows: false,
        hideDisabledCheckboxes: false,
    },
    localeText: AG_GRID_LOCALE_FA,
    //navigateToNextCell: agGridNavigateToNextCell,
    suppressPaginationPanel: true,
    suppressColumnVirtualisation: true,
    defaultExcelExportParams: {
        sheetName: 'Sheet1'
    },
    excelStyles: [
        {
            id: 'stringType',
            dataType: 'string'
        }
    ],
    //onFilterChanged: agGridAutoSizeColumnWidthOnFilterChanged
}

let agGridBooleanFilterParams = {
    valueFormatter: p1 => {
        return p1.value ? "فعال" : "غیر فعال";
    },
    keyCreator: p2 => {
        return p2.value;
    },
    buttons: ['clear']
}

let agGridServerSideBooleanFilterParams = {
    values: [true, false],
    valueFormatter: p1 => {
        return p1.value ? "فعال" : "غیر فعال";
    },
    keyCreator: p2 => {
        return p2.value;
    },
    buttons: ['clear']
}

let agGridYesNoFilterParams = {
    valueFormatter: p1 => {
        return p1.value ? "بله" : "خیر";
    },
    keyCreator: p2 => {
        return p2.value;
    },
    buttons: ['clear']
}

let agGridServerSideYesNoFilterParams = {
    values: [true, false],
    valueFormatter: p1 => {
        return p1.value ? "بله" : "خیر";
    },
    keyCreator: p2 => {
        return p2.value;
    },
    buttons: ['clear']
}
