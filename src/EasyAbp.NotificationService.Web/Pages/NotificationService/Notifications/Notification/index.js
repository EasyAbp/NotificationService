$(function () {

    var l = abp.localization.getResource('EasyAbpNotificationService');

    var service = easyAbp.notificationService.notifications.notification;

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
            // {
            //     rowAction: {
            //         items:
            //             [
            //             ]
            //     }
            // },
            { data: "userId" },
            { data: "notificationInfoId" },
            { data: "notificationMethod" },
            { data: "success" },
            { data: "completionTime" },
            { data: "failureReason" },
            { data: "retryForNotificationId" },
        ]
    }));
});
