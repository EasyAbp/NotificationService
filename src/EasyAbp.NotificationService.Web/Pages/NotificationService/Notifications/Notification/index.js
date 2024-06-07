$(function () {

    $("#NotificationFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    var getFilter = function () {
        var input = {};
        $("#NotificationFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/NotificationFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('EasyAbpNotificationService');

    var service = easyAbp.notificationService.notifications.notification;
    var detailsModal = new abp.ModalManager(abp.appPath + 'NotificationService/Notifications/Notification/DetailsModal');

    var dataTable = $('#NotificationTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,//disable default searchbox
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Details'),
                                action: function (data) {
                                    detailsModal.open({id: data.record.id});
                                }
                            }
                        ]
                }
            },
            {
                title: l('NotificationUserName'),
                data: "userName"
            },
            {
                title: l('NotificationNotificationMethod'),
                data: "notificationMethod"
            },
            {
                title: l('NotificationSuccess'),
                data: "success",
                render: function (data, type, row) {
                    if (data === true) {
                        return '✔️'
                    }
                    if (data === false) {
                        return '❌'
                    }
                    return ''
                }
            },
            {
                title: l('NotificationCompletionTime'),
                data: "completionTime"
            },
            {
                title: l('NotificationFailureReason'),
                data: "failureReason"
            },
        ]
    }));
});
