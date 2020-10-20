function GameLoaded() {
    $("#loader").hide();
    $("#game").removeAttr("style");
    $("#participantInfoFooter").show();
}

function SendMessageToChatScript(message) {
    var response = {
        Message: "Failed to complete Ajax"
    };

    $.ajax({
        type: "POST",
        url: '/Index?handler=SendMessageToChatScript',
        async: false,
        data: message,
        accept: "application/json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (ajaxResponse) {
            response = ajaxResponse;
        },
        error: function (ajaxResponse) {
            console.log("Error");
            console.log(ajaxResponse);
            response = ajaxResponse;
        }
    });

    return response;
}

function EnsureUserCreated(user) {
    $.ajax({
        type: "POST",
        url: '/Index?handler=EnsureUserCreated',
        async: false,
        data: user,
        accept: "application/json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (ajaxResponse) {
            //
        },
        error: function (ajaxResponse) {
            console.log("Error");
            console.log(ajaxResponse);
        }
    });

    SetCookieId(user.CookieId);

    return true;
}

function DeleteCookie() {
    Cookies.remove("id");

    return true;
}

function GetUserFromCookieId() {
    var id = Cookies.get("id");

    if (id == undefined) {
        return null;
    }

    var response = null;

    $.ajax({
        type: "POST",
        url: '/Index?handler=GetUserFromCookieId',
        async: false,
        data: {
            id: id
        },
        accept: "application/json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (ajaxResponse) {
            response = ajaxResponse;
        },
        error: function (ajaxResponse) {
            DeleteCookie();
        }
    });

    return response;
}

function SetCookieId(id) {
    Cookies.set("id", id, { sameSite: 'strict' });

    return true;
}

function SaveUser(user) {
    $.ajax({
        type: "POST",
        url: '/Index?handler=SaveUser',
        async: false,
        data: user,
        accept: "application/json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (ajaxResponse) {
            //
        },
        error: function (ajaxResponse) {
            console.log("Error");
            console.log(ajaxResponse);
        }
    });

    return true;
}

function GameOver(user) {
    // Show survey
    if (user.Survey == null && ($("#survey").data('bs.modal') || { _isShown: false })._isShown == false) {
        $.ajax({
            type: "GET",
            url: "/Index?handler=SurveyModalPartial",
            async: false,
            success: function (data) {
                $("#modal-container").html(data);
                $("#survey").modal('show');
            },
            error: function (data) {
                console.log("Error");
                console.log(data);
            }
        });
    }

    // Set wintime
    $.ajax({
        type: "POST",
        url: '/Index?handler=SetWinTime',
        async: false,
        data: user,
        accept: "application/json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (ajaxResponse) {
            user = ajaxResponse;
        },
        error: function (ajaxResponse) {
            console.log("Error");
            console.log(ajaxResponse);
        }
    });

    return user;
}

function ShowParticipantInformationModal() {
    $.ajax({
        type: "GET",
        url: "/Index?handler=ParticipantInformationModalPartial",
        async: false,
        success: function (data) {
            $("#modal-container").html(data);
            $("#participantInformation").modal('show');
        },
        error: function (data) {
            console.log("Error");
            console.log(data);
        }
    });
}

function IsParticipantInformationModalClosed() {
    return ($("#participantInformation").data('bs.modal') || { _isShown: false })._isShown == false;
}

$(document).ready(function () {
    $("#game").attr("style", "width: 1px; height: 1px;");
    $("#participantInfoFooter").hide();

    $("#modal-container").on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();

        $("#SurveyFormCookieId").val(Cookies.get("id"));
        var form = $(this).parents('.modal').find('form');
        var dataToSend = form.serialize();

        $.ajax({
            type: "POST",
            url: form.attr('action'),
            data: dataToSend,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            success: function (data) {
                $("#modal-container").find('.modal-body').replaceWith($('.modal-body', data));

                var isValid = $("#modal-container").find('.modal-body').find('[name="IsValid"]').val() == 'True';

                if (isValid) {
                    $("#modal-container").find('.modal').modal('hide');
                }
            },
            error: function (data) {
                console.log("Error");
                console.log(ajaxResponse);
            }
        });
    });
});
