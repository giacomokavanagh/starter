$(document).ready(function () {
    toggleExcelSpecificFields(); 
    $("#Test_TestDataSource").change(function () {
        toggleExcelSpecificFields();
    });
});

function toggleExcelSpecificFields() {
    if ($("#Test_TestDataSource").val() == "Excel")
        $("#ExcelSelected").show();
    else
        $("#ExcelSelected").hide();
}

$(document).ready(function () {
    $("#AtEndOfProcedure").change(function () {
        var c = this.checked;
        if (c) {
            $("#SelectStepForImportProcess").hide();
            $("#AtStartOfProcedureForImportProcess").hide();
        }
        else {
            $("#SelectStepForImportProcess").show();
            $("#AtStartOfProcedureForImportProcess").show();
        }
    });
});

$(document).ready(function () {
    $("#AtStartOfProcedure").change(function () {
        var c = this.checked;
        if (c) {
            $("#SelectStepForImportProcess").hide();
            $("#AtEndOfProcedureForImportProcess").hide();
        }
        else {
            $("#SelectStepForImportProcess").show();
            $("#AtEndOfProcedureForImportProcess").show();
        }
    });
});

$(document).ready(function () {

    $(".chosen").chosen({
        width: "90%"
    });
    $(".chosen").chosen().change();
    $("Run_Tests").trigger('chosen:updated');
});

$(document).ready(function () {

    $(".chosensmalldependencygroup").chosen({
        allow_single_deselect: true,
        width: "95%"
    }).change(PostDependencyGroupToModel);
});

function PostDependencyGroupToModel(e) {
    var id = parseInt(this.id);
    var value = parseInt(this.value);
    $.ajax({
        url: '/TestRuns/SetDependencyGroup',
        type: 'POST',
        data: {
            'id': id,
            'value': value
        },
    })
};

$(document).ready(function () {

    $(".chosensmallstepmethod").chosen({
        width: "100%"
    }).change(PostMethodToModel);
});

function PostMethodToModel(e) {
    var id = parseInt(this.id);
    var value = this.value;
    $.ajax({
        url: '/Tests/SetMethod',
        type: 'POST',
        data: {
            'id': id,
            'value': value
        },
    })
};

$(document).ready(function () {

    $(".chosensmallprocessstepmethod").chosen({
        width: "100%"
    }).change(PostProcessStepMethodToModel);
});

function PostProcessStepMethodToModel(e) {
    var id = parseInt(this.id);
    var value = this.value;
    $.ajax({
        url: '/Processes/SetMethod',
        type: 'POST',
        data: {
            'id': id,
            'value': value
        },
    })
};

$(document).ready(function () {

    $(".chosensmallprocedurestepmethod").chosen({
        width: "100%"
    }).change(PostProcedureStepMethodToModel);
});

function PostProcedureStepMethodToModel(e) {
    var id = parseInt(this.id);
    var value = this.value;
    $.ajax({
        url: '/Procedures/SetMethod',
        type: 'POST',
        data: {
            'id': id,
            'value': value
        },
    })
};

$(document).ready(function () {

    $(".chosensingledeselect").chosen({
        allow_single_deselect: true,
        width: "100%"
    });
    $(".chosensingledeselect").chosen().change();
});

$(document).ready(function () {
    $('#DataTable').dataTable();
});

$(document).ready(function () {
    $('#2ColumnDataTable').dataTable({
        "columns": [
            { "width": "30%" },
            { "width": "70%" }
        ]
    });
});

$(document).ready(function () {
    $('#3ColumnDataTable').dataTable({
        "columns": [
            { "width": "8%" },
            { "width": "30%" },
            { "width": "52%" }
        ]
    });
});

$(document).ready(function () {
    $('#AvailableMethodsDataTable').dataTable({
        "lengthMenu": [[10, 25, 50, 100, 250, 500, 1000, -1], [10, 25, 50, 100, 250, 500, 1000, "All"]],
        "pageLength": -1,
        "columns": [
            { "width": "8%" },
            { "width": "30%" },
            { "width": "52%" }
        ]
    });
});

$(document).ready(function () {
    $('#StepsUsingMethodDataTable').dataTable({
        "lengthMenu": [[10, 25, 50, 100, 250, 500, 1000, -1], [10, 25, 50, 100, 250, 500, 1000, "All"]],
        "pageLength": 10,
        "columns": [
            { "width": "8%" },
            { "width": "30%" },
            { "width": "52%" }
        ]
    });
});

$(document).ready(function () {
    $('#ResultsUsingMethodDataTable').dataTable({
        "lengthMenu": [[10, 25, 50, 100, 250, 500, 1000, -1], [10, 25, 50, 100, 250, 500, 1000, "All"]],
        "pageLength": 10,
        "columns": [
            { "width": "10%" },
            { "width": "28%" },
            { "width": "52%" }
        ]
    });
});

$(document).ready(function () {
    $('#TestRunsUsingTestDataTable').dataTable({
        "lengthMenu": [[10, 25, 50, 100, 250, 500, 1000, -1], [10, 25, 50, 100, 250, 500, 1000, "All"]],
        "pageLength": 25,
        "columns": [
            { "width": "30%" },
            { "width": "70%" }
        ]
    });
});

$(document).ready(function () {
    $('#3ColumnDataTableIDDescending').dataTable({
        "columns": [
            { "width": "8%" },
            { "width": "30%" },
            { "width": "52%" }
        ],
        "order": [[0, "desc"]]
    });
});

$(document).ready(function () {
    $('#4ColumnDataTable').dataTable({
        "columns": [
            { "width": "8%" },
            { "width": "20%" },
            { "width": "50%" },
            { "width": "22%" }
        ]
    });
});

$(document).ready(function () {
    $('#TestRunnerGroupDataTable').dataTable({
        "columns": [
            { "width": "4%" },
            { "width": "10%" },
            { "width": "10%" },
            { "width": "10%" },
            { "width": "10%" },
            { "width": "10%" },
            { "width": "10%" }
        ]
    });
});

$(document).ready(function () {
    $("#RightClickTable").contextMenu({
        menuSelector: "#contextMenu",
        menuSelected: function (invokedOn, selectedMenu) {
            RowNode = invokedOn.context.parentNode;
            while (RowNode.tagName != "TR") {
                RowNode = RowNode.parentNode;
            }

            if (this.text == "Details in new tab") {
                url = "/" + selectedMenu.context.name + "/Details/"
                + RowNode.firstElementChild.innerText;
                var win = window.open(url, '_blank');
                win.focus();
            } else {
                window.location.href = "/" + selectedMenu.context.name + "/" + selectedMenu.text() + "/"
                + RowNode.firstElementChild.innerText;
            }
        }
    });
});

$(document).ready(function () {
    $('#TestRunDataTable').dataTable({
        "lengthMenu": [[10, 25, 50, 100, 250, 500, 1000, -1], [10, 25, 50, 100, 250, 500, 1000, "All"]],
        "pageLength": -1,
        dom: 'lBfrtip',
        buttons: [
            'colvis', 'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        "columns": [
            { "width": "4%" },
            { "width": "5.7%" },
            { "width": "6.3%" },
            { "width": "17%" },
            { "width": "7%" },
            { "width": "5.5%" },
            { "width": "7.7%" },
            { "width": "9.5%" },
            { "width": "6%" },
            { "width": "8.5%" },
            { "width": "5%" },
            { "width": "5%" },
            { "width": "9.2%" }
        ],
    });
});

$(document).ready(function () {
    $('#ResultsDataTable').dataTable({
        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
        dom: 'lBfrtip',
        buttons: [
            'colvis', 'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        "columns": [
            { "width": "8%" },
            { "width": "10%" },
            { "width": "20%" },
            { "width": "10%" },
            { "width": "10%" },
            { "width": "15%" },
            { "width": "15%" },
            { "width": "10%" }
        ],
        "order": [[0, "desc"]]
    });
});

$(document).ready(function () {
    $('#StepDataTable').dataTable({
        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
        "pageLength": -1,
        dom: 'lBfrtip',
        buttons: [
            'colvis', 'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        "columns": [
            { "width": "4%" },
            { "width": "10%" },
            { "width": "12%" },
            { "width": "10%" },
            { "width": "10%" },
            { "width": "18%" },
            { "width": "6%" }
        ],
        "order": [[6, "asc"]],
    });
});

$(document).ready(function () {
    $('#ProcessStepDataTable').dataTable({
        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
        "pageLength": -1,
        dom: 'lBfrtip',
        buttons: [
            'colvis', 'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        "columns": [
            { "width": "5%" },
            { "width": "10%" },
            { "width": "12%" },
            { "width": "10%" },
            { "width": "10%" },
            { "width": "17%" },
            { "width": "7%" },
            { "width": "7%" }
        ],
        "order": [[7, "asc"]],
    });
});

$(document).ready(function () {
    $('#ProcedureStepDataTable').dataTable({
        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
        "pageLength": -1,
        dom: 'lBfrtip',
        buttons: [
            'colvis', 'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        "columns": [
            { "width": "5%" },
            { "width": "10%" },
            { "width": "12%" },
            { "width": "10%" },
            { "width": "10%" },
            { "width": "17%" },
            { "width": "7%" },
            { "width": "7%" },
            { "width": "7%" }
        ],
        "order": [[7, "asc"]],
    });
});

$(document).ready(function () {
    $('#TestCaseDataTable').dataTable({
        "lengthMenu": [[10, 25, 50, 100, 250, 500 -1], [10, 25, 50, 100, 250, 500, "All"]],
        "pageLength": -1,
        dom: 'lBfrtip',
        buttons: [
            'colvis', 'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        "order": [[0, "asc"]],
        "columnDefs": [
            {
                "targets": [ 0 ],
                "visible": false,
            },
        ],
    });
});

$(document).ready(function () {
    $("#TestRunDataRows").contextMenu({
        menuSelector: "#contextMenu",
        menuSelected: function (invokedOn, selectedMenu) {
            RowNode = invokedOn.context.parentNode;
            while (RowNode.tagName != "TR") {
                RowNode = RowNode.parentNode;
            }

            if (this.text == "Create Dependency Group")
            {
                window.location.href = "/DependencyGroups";
            }
            else if (this.text == "View Dependency Group")
            {
                window.location.href = "/DependencyGroups/DetailsFromTestRun/" +
                    RowNode.firstElementChild.innerText;
            }
            else
            {
                window.location.href = "/TestRuns/" + selectedMenu.text() + "/"
                    + RowNode.firstElementChild.innerText;
            }
        }
    });
});

$(document).ready(function () {
    $("#StepDataRows").contextMenu({
        menuSelector: "#contextMenu",
        menuSelected: function (invokedOn, selectedMenu) {
            RowNode = invokedOn.context.parentNode;
            while (RowNode.tagName != "TR") {
                RowNode = RowNode.parentNode;
            }
            if (this.text == "Edit Step") {
                window.location.href = "/Tests/EditStep/" +
                RowNode.firstElementChild.innerText;
            }
            else if(this.text == "Move Step Up")
            {
                window.location.href = "/Tests/MoveStepUp/" +
                RowNode.firstElementChild.innerText;
            }
            else if (this.text == "Move Step Down") {
                window.location.href = "/Tests/MoveStepDown/" +
                RowNode.firstElementChild.innerText;
            }
            else if (this.text == "Delete Step") {
                window.location.href = "/Tests/DeleteStep/" +
                RowNode.firstElementChild.innerText;
            }
            else {
                //There are no other options at the moment
            }
        }
    });
});

$(document).ready(function () {
    $("#ProcessStepDataRows").contextMenu({
        menuSelector: "#contextMenu",
        menuSelected: function (invokedOn, selectedMenu) {
            RowNode = invokedOn.context.parentNode;
            while (RowNode.tagName != "TR") {
                RowNode = RowNode.parentNode;
            }
            if (this.text == "Edit Step") {
                window.location.href = "/Processes/EditStep/" +
                RowNode.firstElementChild.innerText;
            }
            else if (this.text == "Move Step Up") {
                window.location.href = "/Processes/MoveStepUp/" +
                RowNode.firstElementChild.innerText;
            }
            else if (this.text == "Move Step Down") {
                window.location.href = "/Processes/MoveStepDown/" +
                RowNode.firstElementChild.innerText;
            }
            else if (this.text == "Delete Step") {
                window.location.href = "/Processes/DeleteStep/" +
                RowNode.firstElementChild.innerText;
            }
            else {
                //There are no other options at the moment
            }
        }
    });
});

$(document).ready(function () {
    $("#ProcedureStepDataRows").contextMenu({
        menuSelector: "#contextMenu",
        menuSelected: function (invokedOn, selectedMenu) {
            RowNode = invokedOn.context.parentNode;
            while (RowNode.tagName != "TR") {
                RowNode = RowNode.parentNode;
            }
            if (this.text == "Edit Step") {
                window.location.href = "/Procedures/EditStep/" +
                RowNode.firstElementChild.innerText;
            }
            else if (this.text == "Move Step Up") {
                window.location.href = "/Procedures/MoveStepUp/" +
                RowNode.firstElementChild.innerText;
            }
            else if (this.text == "Move Step Down") {
                window.location.href = "/Procedures/MoveStepDown/" +
                RowNode.firstElementChild.innerText;
            }
            else if (this.text == "Delete Step") {
                window.location.href = "/Procedures/DeleteStep/" +
                RowNode.firstElementChild.innerText;
            }
            else if (this.text == "Update Step From Process") {
                window.location.href = "/Procedures/UpdateStepFromProcess/" +
                RowNode.firstElementChild.innerText;
            }
            else if (this.text == "Go To Process") {
                window.location.href = "/Procedures/GoToProcessFromProcedureStep/" +
                RowNode.firstElementChild.innerText;
            }
            else {
                //There are no other options at the moment
            }
        }
    });
});

$(document).ready(function () {
    $("#DerivedKeyDataRows").contextMenu({
        menuSelector: "#contextMenu",
        menuSelected: function (invokedOn, selectedMenu) {
            RowNode = invokedOn.context.parentNode;
            while (RowNode.tagName != "TR") {
                RowNode = RowNode.parentNode;
            }

            if (this.text == "Delete Derived Key") {
                window.location.href = "/MasterKeys/DeleteDerivedKey/" +
                RowNode.firstElementChild.innerText;
            }
            else {
                //There are no other options at the moment
            }
        }
    });
});

$(document).ready(function () {
    $("#ProcessesInProcedureDataRows").contextMenu({
        menuSelector: "#contextMenu2",
        menuSelected: function (invokedOn, selectedMenu) {
            RowNode = invokedOn.context.parentNode;
            while (RowNode.tagName != "TR") {
                RowNode = RowNode.parentNode;
            }
            if (this.text == "Delete Process In Procedure") {
                window.location.href = "/Procedures/DeleteProcessFromProcedure/" +
                RowNode.firstElementChild.innerText;
            }
            else if (this.text == "Update Procedure From Process") {
                window.location.href = "/Procedures/UpdateProcedureFromProcess/?id=" +
                RowNode.firstElementChild.innerText + "&redirect=Process";
            }
            else if (this.text == "Update Process In Procedure") {
                window.location.href = "/Procedures/UpdateProcedureFromProcess/?id=" +
                RowNode.firstElementChild.innerText + "&redirect=Procedure";
            }
            else if (this.text == "Disassociate Process From Procedure") {
                window.location.href = "/Procedures/DisassociateProcedureFromProcess/?id=" +
                RowNode.firstElementChild.innerText + "&redirect=Procedure";
            }
            else if (this.text == "Disassociate Procedure In Process") {
                window.location.href = "/Procedures/DisassociateProcedureFromProcess/?id=" +
                RowNode.firstElementChild.innerText + "&redirect=Process";
            }
            else {
                //There are no other options at the moment
            }
        }
    });
});

(function ($, window) {

    $.fn.contextMenu = function (settings) {

        return this.each(function () {

            // Open context menu
            $(this).on("contextmenu", function (e) {
                // return native menu if pressing control
                if (e.ctrlKey) return;

                //open menu
                var $menu = $(settings.menuSelector)
                    .data("invokedOn", $(e.target))
                    .show()
                    .css({
                        position: "absolute",
                        left: getMenuPosition(e.clientX, 'width', 'scrollLeft'),
                        top: getMenuPosition(e.clientY, 'height', 'scrollTop')
                    })
                    .off('click')
                    .on('click', 'a', function (e) {
                        $menu.hide();

                        var $invokedOn = $menu.data("invokedOn");
                        var $selectedMenu = $(e.target);

                        settings.menuSelected.call(this, $invokedOn, $selectedMenu);
                    });

                return false;
            });

            //make sure menu closes on any click
            $(document).click(function () {
                $(settings.menuSelector).hide();
            });
        });

        function getMenuPosition(mouse, direction, scrollDir) {
            var win = $(window)[direction](),
                scroll = $(window)[scrollDir](),
                menu = $(settings.menuSelector)[direction](),
                position = mouse + scroll;

            // opening menu would pass the side of the page
            if (mouse + menu > win && menu < mouse)
                position -= menu;

            return position;
        }

    };
})(jQuery, window);

$(document).ready(function () {
    $(function () {
        $('.datetimepickerinput').datetimepicker({
            useCurrent: false,
            showTodayButton: true,
            showClear:true,
            showClose: true,
            format: 'DD-MM-YYYY HH:mm:ss'
        });
        $('.datetimepickerinput').on('dp.change', PostStartTimeToModel);
    });
});

function PostStartTimeToModel(e) {
    var id = parseInt(this.id);
    var time = this.value;
    $.ajax({
        url: '/TestRuns/UpdateTimeFromPicker',
        type: 'POST',
        data: {
            'id' : id,
            'time': time
        },
    })
};

$(document).ready(function () {
    $(function () {
        $('.datetimepicker').datetimepicker({
            useCurrent: false,
            showTodayButton: true,
            showClear:true,
            showClose: true,
            format: 'DD-MM-YYYY HH:mm:ss'
        });
    });
});

$(document).ready(function () {
    $(function () {
        RunTestButtons = document.getElementsByClassName('RunTestsButton');
        for (var i = 0; i < RunTestButtons.length; i++) {
            RunTestButtons[i].addEventListener('click', ClickRunButton, false);
        }
    });
});
            
function ClickRunButton(e) {
    var id = parseInt(this.getAttribute("name"));
    var now = moment().format("DD-MM-YYYY HH:mm:ss").toString();
    $.ajax({
        url: '/TestRuns/RunTestNow',
        type: 'POST',
        data: {
            'id': id,
            'time': now
        },
        result: { get_param: 'value' },
        success: function (result) {
            document.getElementById(TestStatusID).innerText = result;
            document.getElementById(TestStatusID).className = "";
        }
    });
        
    var TestStatusID = "TestStatus" + id.toString();
    document.querySelector('.datetimepickerinput[id$="' + id + '"]').value = now;
    var Retries = document.querySelector('#Retries' + id).value;
    document.getElementById('RetriesLeftTD' + id.toString()).innerText = Retries;
    $('div[name=' + "RunTestDiv" + id.toString()).hide();
    $('div[name=' + "StopTestDiv" + id.toString()).show();
};

$(document).ready(function () {
    $(function () {
        StopTestButtons = document.getElementsByClassName('StopTestsButton');
        for (var i = 0; i < StopTestButtons.length; i++) {
            StopTestButtons[i].addEventListener('click', ClickStopButton, false);
        }
    });
});

function ClickStopButton(e) {
    var id = parseInt(this.getAttribute("name"));
    $.ajax({
        url: '/TestRuns/StopTestNow',
        type: 'POST',
        data: { 'id': id },
    })
    var TestStatusID = "TestStatus" + id.toString();
    document.getElementById(TestStatusID).innerText = "Unassigned";
    document.getElementById('RetriesLeftTD' + id.toString()).innerText = "";
    $('div[name=' + "RunTestDiv" + id.toString()).show();
    $('div[name=' + "StopTestDiv" + id.toString()).hide();
};

$(document).ready(function () {
    $(".Retriesinput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = parseInt(this.value);
        $.ajax({
            url: '/TestRuns/SetRetries',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".stepIDInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = this.value;
        $.ajax({
            url: '/Tests/SetStepID',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".attributeInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = this.value;
        $.ajax({
            url: '/Tests/SetAttribute',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".valueInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = this.value;
        $.ajax({
            url: '/Tests/SetValue',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".inputInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = this.value;
        $.ajax({
            url: '/Tests/SetInput',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".orderInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = parseInt(this.value);
        $.ajax({
            url: '/Tests/SetOrder',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".ProcessStepStepIDInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = this.value;
        $.ajax({
            url: '/Processes/SetStepID',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".ProcessStepAttributeInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = this.value;
        $.ajax({
            url: '/Processes/SetAttribute',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".ProcessStepValueInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = this.value;
        $.ajax({
            url: '/Processes/SetValue',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".ProcessStepInputInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = this.value;
        $.ajax({
            url: '/Processes/SetInput',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".ProcessStepStaticInput").change(function () {

        var id = this.getAttribute("id");
        var value = this.checked;
        $.ajax({
            url: '/Processes/SetStatic',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".ProcessStepOrderInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = parseInt(this.value);
        $.ajax({
            url: '/Processes/SetOrder',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".ProcedureStepStepIDInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = this.value;
        $.ajax({
            url: '/Procedures/SetStepID',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".ProcedureStepAttributeInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = this.value;
        $.ajax({
            url: '/Procedures/SetAttribute',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".ProcedureStepValueInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = this.value;
        $.ajax({
            url: '/Procedures/SetValue',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".ProcedureStepInputInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = this.value;
        $.ajax({
            url: '/Procedures/SetInput',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".ProcedureStepStaticInput").change(function () {

        var id = this.getAttribute("id");
        var value = this.checked;
        $.ajax({
            url: '/Procedures/SetStatic',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $(".ProcedureStepOrderInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = parseInt(this.value);
        $.ajax({
            url: '/Procedures/SetOrder',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {

    $(".chosenTestRunnerKey").chosen({
    }).change(PostTestRunnerToModel);
});

function PostTestRunnerToModel(e) {
    var id = parseInt(this.id);
    var value = parseInt(this.value);
    $.ajax({
        url: '/MasterKeys/SetTestRunner',
        type: 'POST',
        data: {
            'id': id,
            'value': value
        },
    })
};

$(document).ready(function () {
    $(".TestCaseDataInput").change(function () {

        var id = parseInt(this.getAttribute("name"));
        var value = this.value;
        $.ajax({
            url: '/TestCases/SetData',
            type: 'POST',
            data: {
                'id': id,
                'value': value
            },
        })
    });
});

$(document).ready(function () {
    $("#RightClickTreeview").contextMenu({
        menuSelector: "#contextMenu",
        menuSelected: function (invokedOn, selectedMenu) {

            var node = invokedOn.context;
            var name = node.name;
            var array = name.split('-');
            var controller = array[0];
            var id = array[1];

            if (this.text == "Details in new tab") {
                url = "/" + controller + "/Details/" + id;
                var win = window.open(url, '_blank');
                win.focus();
            } else {
                window.location.href = "/" + controller + "/" + selectedMenu.text() + "/"
                    + id;
            }
        }
    });
});

$(document).ready(function () {
    $('#PageObjectDataTable').dataTable({
        "lengthMenu": [[10, 25, 50, 100, 250, 500, 1000, -1], [10, 25, 50, 100, 250, 500, 1000, "All"]],
        "pageLength": -1,
        dom: 'lBfrtip',
        buttons: [
            'colvis', 'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        "columns": [
            { "width": "4%" },
            { "width": "5.7%" },
            { "width": "6.3%" },
            { "width": "17%" },
            { "width": "7%" },
            { "width": "5.5%" },
            { "width": "7.7%" },
            { "width": "9.5%" },
            { "width": "6%" },
            { "width": "8.5%" },
            { "width": "5%" },
            { "width": "5%" },
            { "width": "9.2%" }
        ],
    });
});


$(document).ready(function () {
    $("#Toggle.treeviewLink").click(function () {
        var className = this.className;
        var id = this.name;
        var isExpanded = false;
        if (className == "treeviewLink") {
            isExpanded = false;
        } else {
            isExpanded = true;
        }

        $.ajax({
            url: '/GenericFolders/SetTreeviewNodeState',
            type: 'POST',
            data: {
                'id': id,
                'isExpanded': isExpanded
            },
        })
    });
});