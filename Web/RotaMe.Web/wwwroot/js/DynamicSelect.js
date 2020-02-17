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

    $('.project-remove-user-selector').ready(function (event) {
        var users = $('.project-remove-user-selector').children("option:selected").data('users');
        var userSelect = $('.user-remove-selector');
        userSelect.empty();

        for (var i = 0; i < users.length; i++) {
            var userName = users[i];
            userSelect.append($("<option></option>")
                .attr("value", userName)
                .text(userName));
        }
    });
    $('.project-remove-user-selector').on('change', function (event) {
        var users = $('.project-remove-user-selector').children("option:selected").data('users');
        var userSelect = $('.user-remove-selector');
        userSelect.empty();

        for (var i = 0; i < users.length; i++) {
            var userName = users[i];
            userSelect.append($("<option></option>")
                .attr("value", userName)
                .text(userName));
        }
    });
});
