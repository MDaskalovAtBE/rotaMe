'use strict';
$(document).ready(function () {
    $('.edit-event-need-tab').on('click', function (event) {
        var id = this.id.substr(5);
        var date = $('#NeedDate_' + id)[0].innerText;
        var minUsers = $('#NeedMinUsers_' + id)[0].innerText;
        var maxUsers = $('#NeedMaxUsers_' + id)[0].innerText;

        $('#NeedId').val(id);
        $('#MinUsers').val(minUsers);
        $('#MaxUsers').val(maxUsers);
        $('#NeedDate').val(date);
    });
});
