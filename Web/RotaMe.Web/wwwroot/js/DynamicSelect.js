'use strict';
$(document).ready(function () {
    $('.user-unassign-selector').ready(function (event) {
        var roles = $('.user-unassign-selector').children("option:selected").data('roles');
        var roleSelect = $('.role-unassign-selector');
        roleSelect.empty();

        for (var i = 0; i < roles.length; i++) {
            var roleName = roles[i];
            roleSelect.append($("<option></option>")
                .attr("value", roleName)
                .text(roleName)); 
        }


    });
    $('.user-unassign-selector').on('change', function (event) {
        var roles = $('.user-unassign-selector').children("option:selected").data('roles');
        var roleSelect = $('.role-unassign-selector');
        roleSelect.empty();

        for (var i = 0; i < roles.length; i++) {
            var roleName = roles[i];
            roleSelect.append($("<option></option>")
                .attr("value", roleName)
                .text(roleName));
        }


    });
});
