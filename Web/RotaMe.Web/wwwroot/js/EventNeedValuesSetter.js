'use strict';
$(document).ready(function () {
    $('.edit-event-need-tab').on('click', function (event) {
        var id = this.id.substr(5);
        var dateTime = $('#NeedDate_' + id)[0].innerText;
        var minUsers = $('#NeedMinUsers_' + id)[0].innerText;
        var maxUsers = $('#NeedMaxUsers_' + id)[0].innerText;
        var date = dateTime.slice(0, 10);
        var hours = dateTime.substr(11, 2);
        console.log(date);
        console.log(hours);
        $('#NeedId').val(id);
        $('#MinUsers').val(minUsers);
        $('#MaxUsers').val(maxUsers);
        $('#event-edit-date').datetimepicker({
            value: date,
            format: 'd/m/yy H:i',
            inline: true,
            lang: 'bg'
        });
    });
});
