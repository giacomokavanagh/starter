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
    $('#4ColumnDataTable').dataTable({
        "columns": [
            { "width": "8%" },
            { "width": "30%" },
            { "width": "52%" },
            { "width": "30%" }
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

            window.location.href = "/" + selectedMenu.context.name + "/" + selectedMenu.text() + "/"
                + RowNode.firstElementChild.innerText;
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
            { "width": "5%" },
            { "width": "5.5%" },
            { "width": "6.5%" },
            { "width": "17%" },
            { "width": "7%" },
            { "width": "5.5%" },
            { "width": "7.4%" },
            { "width": "8%" },
            { "width": "8%" },
            { "width": "8%" },
            { "width": "5%" },
            { "width": "5%" },
            { "width": "7%" }
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
    });
});

$(document).ready(function () {
    $("#TestRunDataRows").contextMenu({
        menuSelector: "#contextMenu",
        menuSelected: function (invokedOn, selectedMenu) {

            if (invokedOn.text() == "Create Dependency Group")
            {
                window.location.href = "/DependencyGroups/Create";
            }
            else if (invokedOn.text() == "Edit Dependency Group")
            {
                window.location.href = "/DependencyGroups/EditFromTestRunID/" + invokedOn.text();
            }
            else
            {
                window.location.href = "/TestRuns/" + selectedMenu.text() + "/"
                    + invokedOn.context.parentNode.firstElementChild.innerText;
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