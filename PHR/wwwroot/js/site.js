
var loaderShow = () => {
    $("#loader").show();
}

var loaderHide = () => {
    $("#loader").hide();
}

$(document).ready(() => {
    $("#webLink").val(window.location.origin);
    loaderHide();
    setTimeout(() => {
        $("#errorMsg").text("");
        $("#userEmail").val("");
    }, 3000);
})

$("#loginButton").click(function (event) {
    errorCount = 0;

    if ($("#userEmail").val() == '') {
        $("#userEmailErr").text('Email is required!');
        errorCount += 1;
    }

    if ($("#userPassword").val() == '') {
        $("#userPasswordErr").text('Password is required!');
        errorCount += 1;
    }

    if (errorCount > 0) {
        return event.preventDefault();
    }
    loaderShow();
    $.ajax({
        url: '/Login/ValidateLoginDetails',
        type: 'POST',
        data: $('#loginForm').serialize(),
        success: function (result) {
            loaderHide();
            if (result.isSuccessful) {
                displayToastMessages("success.svg", "success", "Success", result.message);
                setTimeout(function () {
                    if (result.data.userRole == "SuperAdmin") {
                        window.location.href = "/Home/Home"
                    }
                    else {
                        window.location.href = "/Home/JobSearch"
                    }
                }, 2000);

            } else {
                displayToastMessages("error.svg", "error", "Error", result.message);
            }
        },
        error: function () {
            loaderHide();
        }
    });

})

function isEmailValid(emailId) {
    var regex = /^[a-zA-Z0-9-_]+(\.[a-zA-Z0-9-]+)*@[a-zA-Z0-9-_]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3}))$/;

    var email = $("#" + emailId).val();
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}

function clearErrMessage(id) {
    if (id == 1) {
        if (!isEmailValid("userEmail")) {
            $("#userEmailErr").text('Please enter valid Email!');
        } else {
            $("#userEmailErr").text('');
        }
    } else if (id == 2) {
        $("#userPasswordErr").text('');
    }
}

function displayToastMessages(imgName, type, title, message) {
    cuteToast({
        imgName: imgName,
        type: type,     //"success" or 'info', 'error', 'warning'
        title: title,
        message: message,
        timer: 5000
    })
}

function actionDisplayMessage(type, title, message, confirmBtnName, cancleBtnName, img) {
    var confitmationValue = false;
    cuteAlert({
        type: type,     //"question",
        title: title,   //"Confirm Title",
        message: message,   //"Confirm Message",
        img: img,
        confirmText: confirmBtnName,    //"Okay",
        cancelText: cancleBtnName   //"Cancel"
    }).then((e) => {
        if (e == "confirm") {
            confitmationValue = true;
        } else {
            confitmationValue = false;
        }
    })
    return confitmationValue;
}

function showLoader() {
    cuteAlertLoader();
}

function hideLoader() {
    $("#loaderPopup").hide();
}


$("#register").click(function (event) {

    errorCount = 0;

    if ($("#userName").val() == '') {
        $("#userNameErr").text('User name is required!');
        errorCount += 1;
    }

    if ($("#email").val() == '') {
        $("#userEmailErr").text('Email is required!');
        errorCount += 1;
    }

    if ($("#mobileNo").val() == '') {
        $("#userMobileNoErr").text('Mobile number is required!');
        errorCount += 1;
    }

    if ($("#gender").val() == '') {
        $("#userGenderErr").text('Gender is required!');
        errorCount += 1;
    }

    if ($("#password").val() == '') {
        $("#userPasswordErr").text('Password is required!');
        errorCount += 1;
    }

    if ($("#confirmPassword").val() == '') {
        $("#userCPasswordErr").text('Password is required!');
        errorCount += 1;
    }

    if (errorCount > 0) {
        return event.preventDefault();
    }

    //showLoader();
    loaderShow();
    $.ajax({
        url: '/Login/Register',
        type: 'POST',
        data: $('#userRegistration').serialize(),
        success: function (result) {
            //hideLoader();
            loaderHide();
            if (result.isSuccessful) {
                displayToastMessages("success.svg", "success", "Success", result.message);
                setTimeout(function () {
                    window.location.href = "/home/ApplyNow"
                }, 2000);

            } else {
                displayToastMessages("error.svg", "error", "Error", result.message);
            }
        },
        error: function () {
            loaderHide();
        }
    });
})


function clearErrMsgForRegi(id) {
    if (id == 1) {
        if (!isEmailValid("email")) {
            $("#userEmailErr").text('Please enter valid Email!');
        } else {
            $("#userEmailErr").text('');
        }
    } else if (id == 2) {
        $("#userNameErr").text('');
    }
    else if (id == 3) {
        if (!vaidateMobileNo($("#mobileNo").val())) {
            $("#userMobileNoErr").text('Please enter valid mobile number!');
        } else {
            $("#userMobileNoErr").text('');
        }
    } else if (id == 4) {
        $("#userGenderErr").text('');
    } else if (id == 5) {
        $("#userPasswordErr").text('');
    } else if (id == 6) {
        if ($("#password").val() != $("#confirmPassword").val()) {
            $("#userCPasswordErr").text('Password doesn\'t match');
        } else {
            $("#userCPasswordErr").text('');
        }
    }
}

function vaidateMobileNo(mobileNum) {
    var validateMobNum = /^\d*(?:\.\d{1,2})?$/;
    if (validateMobNum.test(mobileNum) && mobileNum.length == 10) {
        return true;
    }
    else {
        return false;
    }
}

function open_side_panel() {
    $(document).ready(function () {
        $(".modal a").not(".dropdown-toggle").on("click", function () {
            $(".modal").modal("hide");
        });
    });
}

$("#userEmail").on("click", () => {
    $("#errorMsg").text("");
})

function validateConfirmPassword() {
    if ($("#newPassword").val() != $("#confirmPassword").val()) {
        $("#confirmPasswordErr").text('Password doesn\'t match');
    } else {
        $("#confirmPasswordErr").text('');
    }
}

function clearTheErrorMsg(Id) {

    if (Id == 1) {
        $("#nameErr").text('');
    } else if (Id == 2) {
        if (!isEmailValid("email")) {
            $("#emailErr").text('Please enter valid Email!');
        } else {
            $("#emailErr").text('');
        }
    } else if (Id == 3) {
        if (!vaidateMobileNo($("#mobileNo").val())) {
            $("#mobileNoErr").text('Please enter valid Mobile number!');
        } else {
            $("#mobileNoErr").text('');
        }
    }
    else {
        $("#messageErr").text('');
    }
}

function sendEnquiryEmail() {
    var name = $("#guestName").val();
    var email = $("#email").val();
    var enquiry = $("#message").val();
    var mobile = $("#mobileNo").val();
    var count = 0;
    if (name == "") {
        count += 1;
        $("#nameErr").text("Name is required");
    }

    if (email == "") {
        count += 1;
        $("#emailErr").text("Email is required");
    }

    if (enquiry == "") {
        count += 1;
        $("#messageErr").text("Enquiry message is required");
    }

    if (mobile == "") {
        count += 1;
        $("#mobileNoErr").text("Mobile number is required");
    }

    if (count > 0) {
        return;
    }
    var emailDetails = {
        Name: name,
        Email: email,
        Mobile: mobile,
        EnquiryDetails: enquiry
    };

    loaderShow();

    $.post("/Login/SendEnquireyEmail", emailDetails, (result) => {
        loaderHide();
        if (result.isSuccessful == true) {
            $("#guestName").val("");
            $("#email").val("");
            $("#mobleNo").val("");
            $("#message").val("");
            displayToastMessages("success.svg", "success", "Success", result.message);
            $(".modal").modal("hide");
            $(".modal-backdrop").remove()
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
        }
    }).error(() => {
        loaderHide();
        console.log(err);
        displayToastMessages("error.svg", "error", "Error", "Error occuerd, please contact the Administrator");
    })

}

