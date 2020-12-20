$(function () {

    var l = abp.localization.getResource('EasyAbpNotificationService');

    var service = easyAbp.notificationService.notifications.notification;
    var createModal = new abp.ModalManager(abp.appPath + 'NotificationService/Notifications/Notification/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'NotificationService/Notifications/Notification/EditModal');

    var dataTable = $('#NotificationTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('NotificationService.Notification.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('NotificationService.Notification.Delete'),
                                confirmMessage: function (data) {
                                    return l('NotificationDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                    service.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            { data: "userId" },
            { data: "notificationInfoId" },
            { data: "notificationMethod" },
            { data: "success" },
            { data: "completionTime" },
            { data: "failureReason" },
            { data: "retryNotificationId" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewNotificationButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
