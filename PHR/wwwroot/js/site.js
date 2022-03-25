// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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
    showLoader();
    $.ajax({
        url:'/Login/ValidateLoginDetails',
        type: 'POST',
        data: $('#loginForm').serialize(),
        success: function (result) {
            hideLoader();
            if (result.isSuccessful) {
                displayToastMessages("success.svg", "success", "Success", result.message);
                setTimeout(function(){
                    window.location.href = "/"
                },2000);
                
            } else {
                displayToastMessages("error.svg", "error", "Error", result.message);
            }            
        },
        error: function () {
            hideLoader();
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
    } else if (id==2) {
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

function actionDisplayMessage(type,title,message,confirmBtnName,cancleBtnName,img) {
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

//function Toast() {

//    cuteToast({
//        imgName:"error.svg",
//        title:"Success",
//        type: "error", // or 'info', 'error', 'warning'
//        message: "Toast Message",
//        timer: 5000
//    })
//}


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

    showLoader();
    $.ajax({
        url: '/Login/Register',
        type: 'POST',
        data: $('#userRegistration').serialize(),
        success: function (result) {
            hideLoader();
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
            hideLoader();
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

function open_side_panel()
{
    $(document).ready(function () {
        $(".modal a").not(".dropdown-toggle").on("click", function () {
            $(".modal").modal("hide");
        });
    });
}
