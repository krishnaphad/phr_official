//--------------------------------------------------Common loader methods start--------------------------------------------------
var loaderShow = () => {
    $("#loader").show();
}

var LoaderHide = () => {
    $("#loader").hide();
}
//--------------------------------------------------Common loader methods end--------------------------------------------------

//--------------------------------------------------Education Master methods start--------------------------------------------------
$(document).ready(() => {
    $("#educationMasterForm").hide();
})

$("#educationMasterAddBtn").on("click", () => {
    $("#educationMasterForm").show(2000);
    $("#educationMasterAddBtn").hide(2000);
})

$("#educationMasterCancelBtn").on("click", () => {
    $("#educationMasterForm").hide(2000);
    $("#educationMasterAddBtn").show(2000);
    $("#educationNameError").text("");
    $("#educationName").val("");
    $('#isEducationActive').prop('checked', false);
    $("#educationId").val(0);
    $("#educationMasterSubmitBtn").text("Submit");
})

$("#educationMasterSubmitBtn").on("click", () => {
    var educationName = $("#educationName").val();
    if (educationName == "" || educationName == null) {
        $("#educationNameError").text("Degree name is required");
        return;
    }

    var educationData = {
        educationName: educationName,
        IsEducationActive: $('#isEducationActive').is(":checked"),
        educationId: $("#educationId").val()
    }

    loaderShow();
    $.post("/Dashboard/AddEducation", educationData, (result) => {
        LoaderHide();
        if (result.isSuccessful == true) {
            $("#educationName").val("");
            $('#isEducationActive').prop('checked', false);
            $("#educationId").val(0);
            $("#educationMasterSubmitBtn").text("Submit");
            $("#educationMasterForm").hide(2000);
            $("#educationMasterAddBtn").show(2000);
            displayToastMessages("success.svg", "success", "Success", result.message);
            getEducationList($("#currentPageNo").val(), 10);
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
        }
    })
})

$("#educationName").on("keypress", () => {
    $("#educationNameError").text("");
})

function deleteEducation(id) {

    cuteAlert({
        type: "question",     //"question",
        title: "Degree Delete Confirmation",   //"Confirm Title",
        message: "Are you sure you want to delete this degree?",   //"Confirm Message",
        img: "question.svg",
        confirmText: "Yes",    //"Okay",
        cancelText: "No"   //"Cancel"
    }).then((e) => {
        if (e == "confirm") {
            loaderShow();
            $.get("/Dashboard/DeleteEducation?educationMasterId=" + id, (result) => {
                LoaderHide();
                if (result.isSuccessful == true) {
                    displayToastMessages("success.svg", "success", "Success", result.message);
                    getEducationList($("#currentPageNo").val(), 10);
                } else {
                    displayToastMessages("error.svg", "error", "Error", result.message);
                }
            })
        }
    })
}

function editEducation(cityId) {
    if ($(document).scrollTop() > 0) {
        $(document).scrollTop(-$(document).scrollTop());
    }

    loaderShow();
    $.get("/Dashboard/EditEducation?educationMasterId=" + cityId, (result) => {
        LoaderHide();
        if (result.isSuccessful == true) {
            displayToastMessages("success.svg", "success", "Success", result.message);
            $("#educationName").val(result.data.educationName);
            $('#isEducationActive').prop('checked', result.data.isEducationActive);
            $("#educationId").val(result.data.educationId);
            $("#educationMasterSubmitBtn").text("Update");
            $("#educationMasterForm").show(2000);
            $("#educationMasterAddBtn").hide(2000);
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
        }
    })
}

function activateDeactivateEducation(id) {
    loaderShow();
    $.get("/Dashboard/ActivateDeactivateEducation?educationMasterId=" + id, (result) => {
        LoaderHide();
        if (result.isSuccessful == true) {
            getEducationList($("#currentPageNo").val(), 10);
            displayToastMessages("success.svg", "success", "Success", result.message);
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
        }
    })
}

function getEducationList(pageNumber, pageSize) {
    loaderShow();
    $.get("/dashboard/GetEducations?pageNumber=" + pageNumber + "&pageSize=" + pageSize, (result) => {
        $("#educationList").html(result);
        LoaderHide();
    });
}
//--------------------------------------------------Education Master methods end-------------------------------------------

//--------------------------------------------------City Master methods start--------------------------------------------------

$(document).ready(() => {
    $("#cityMasterForm").hide();
})

$("#cityMasterAddBtn").on("click", () => {
    $("#cityMasterForm").show(2000);
    $("#cityMasterAddBtn").hide(2000);
})

$("#cityMasterCancelBtn").on("click", () => {
    $("#cityMasterForm").hide(2000);
    $("#cityMasterAddBtn").show(2000);
    $("#cityNameError").text("");
    $("#cityName").val("");
    $('#isCityActive').prop('checked', false);
    $("#cityId").val(0);
    $("#cityMasterSubmitBtn").text("Submit");
})

$("#cityMasterSubmitBtn").on("click", () => {
    var cityName = $("#cityName").val();
    if (cityName == "" || cityName == null) {
        $("#cityNameError").text("City name is required");
        return;
    }

    var cityData = {
        cityName: cityName,
        cityIsActive: $('#isCityActive').is(":checked"),
        cityId: $("#cityId").val()
    }

    loaderShow();
    $.post("/Dashboard/AddCity", cityData, (result) => {
        LoaderHide();
        if (result.isSuccessful == true) {
            $("#cityName").val("");
            $('#isCityActive').prop('checked', false);
            $("#cityId").val(0);
            $("#cityMasterSubmitBtn").text("Submit");
            $("#cityMasterForm").hide(2000);
            $("#cityMasterAddBtn").show(2000);
            displayToastMessages("success.svg", "success", "Success", result.message);
            getCityList($("#currentPageNo").val(), 10);
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
        }
    })
})

$("#cityName").on("keypress", () => {
    $("#cityNameError").text("");
})

function deleteCity(id) {

    cuteAlert({
        type: "question",     //"question",
        title: "City Delete Confirmation",   //"Confirm Title",
        message: "Are you sure you want to delete this city?",   //"Confirm Message",
        img: "question.svg",
        confirmText: "Yes",    //"Okay",
        cancelText: "No"   //"Cancel"
    }).then((e) => {
        if (e == "confirm") {
            loaderShow();
            $.get("/Dashboard/DeleteCity?cityMasterId=" + id, (result) => {
                LoaderHide();
                if (result.isSuccessful == true) {
                    displayToastMessages("success.svg", "success", "Success", result.message);
                    getCityList($("#currentPageNo").val(), 10);
                } else {
                    displayToastMessages("error.svg", "error", "Error", result.message);
                }
            })
        }
    })
}

function editCity(cityId) {
    if ($(document).scrollTop() > 0) {
        $(document).scrollTop(-$(document).scrollTop());
    }

    loaderShow();
    $.get("/Dashboard/EditCity?cityMasterId=" + cityId, (result) => {
        LoaderHide();
        if (result.isSuccessful == true) {
            displayToastMessages("success.svg", "success", "Success", result.message);
            $("#cityName").val(result.data.cityName);
            $('#isCityActive').prop('checked', result.data.cityIsActive);
            $("#cityId").val(result.data.cityId);
            $("#cityMasterSubmitBtn").text("Update");
            $("#cityMasterForm").show(2000);
            $("#cityMasterAddBtn").hide(2000);
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
        }
    })
}

function activateDeactivateCity(id) {
    loaderShow();
    $.get("/Dashboard/ActivateDeactivateCity?cityMasterId=" + id, (result) => {
        LoaderHide();
        if (result.isSuccessful == true) {
            getCityList($("#currentPageNo").val(), 10);
            displayToastMessages("success.svg", "success", "Success", result.message);
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
        }
    })
}

function getCityList(pageNumber, pageSize) {
    loaderShow();
    $.get("/dashboard/GetCities?pageNumber=" + pageNumber + "&pageSize=" + pageSize, (result) => {
        $("#cityList").html(result);
        LoaderHide();
    });
}
//--------------------------------------------------City Master methods end-------------------------------------------

//--------------------------------------------------Key Skill Master methods start--------------------------------------------------

$(document).ready(() => {
    $("#keySkillMasterForm").hide();
})

$("#keySkillMasterAddBtn").on("click", () => {
    $("#keySkillMasterForm").show(2000);
    $("#keySkillMasterAddBtn").hide(2000);
})

$("#keySkillMasterCancelBtn").on("click", () => {
    $("#keySkillMasterForm").hide(2000);
    $("#keySkillMasterAddBtn").show(2000);
    $("#keySkillNameError").text("");
    $("#keySkillName").val("");
    $('#isKeySkillActive').prop('checked', false);
    $("#keySkillId").val(0);
    $("#keySkillMasterSubmitBtn").text("Submit");
})

$("#keySkillMasterSubmitBtn").on("click", () => {
    var keySkillName = $("#keySkillName").val();
    if (keySkillName == "" || keySkillName == null) {
        $("#keySkillNameError").text("Key skill name is required");
        return;
    }

    var keySkillData = {
        keySkillName: keySkillName,
        isKeySkillActive: $('#isKeySkillActive').is(":checked"),
        keySkillId: $("#keySkillId").val()
    }

    loaderShow();
    $.post("/Dashboard/AddKeySkill", keySkillData, (result) => {
        LoaderHide();
        if (result.isSuccessful == true) {
            $("#keySkillName").val("");
            $('#isKeySkillActive').prop('checked', false);
            $("#keySkillId").val(0);
            $("#keySkillMasterSubmitBtn").text("Submit");
            $("#keySkillMasterForm").hide(2000);
            $("#keySkillMasterAddBtn").show(2000);
            displayToastMessages("success.svg", "success", "Success", result.message);
            getKeySkillList($("#currentPageNo").val(), 10);
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
        }
    })
})

$("#keySkillName").on("keypress", () => {
    $("#keySkillNameError").text("");
})

function deleteKeySkill(id) {

    cuteAlert({
        type: "question",     //"question",
        title: "Key Skill Delete Confirmation",   //"Confirm Title",
        message: "Are you sure you want to delete this key skill?",   //"Confirm Message",
        img: "question.svg",
        confirmText: "Yes",    //"Okay",
        cancelText: "No"   //"Cancel"
    }).then((e) => {
        if (e == "confirm") {
            loaderShow();
            $.get("/Dashboard/DeleteKeySkill?keySkillMasterId=" + id, (result) => {
                LoaderHide();
                if (result.isSuccessful == true) {
                    displayToastMessages("success.svg", "success", "Success", result.message);
                    getKeySkillList($("#currentPageNo").val(), 10);
                } else {
                    displayToastMessages("error.svg", "error", "Error", result.message);
                }
            })
        }
    })
}

function editKeySkill(keySkillId) {
    if ($(document).scrollTop() > 0) {
        $(document).scrollTop(-$(document).scrollTop());
    }

    loaderShow();
    $.get("/Dashboard/EditKeySkill?keySkillMasterId=" + keySkillId, (result) => {
        LoaderHide();
        if (result.isSuccessful == true) {
            displayToastMessages("success.svg", "success", "Success", result.message);
            $("#keySkillName").val(result.data.keySkillName);
            $('#isKeySkillActive').prop('checked', result.data.isKeySkillActive);
            $("#keySkillId").val(result.data.keySkillId);
            $("#keySkillMasterSubmitBtn").text("Update");
            $("#keySkillMasterForm").show(2000);
            $("#keySkillMasterAddBtn").hide(2000);
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
        }
    })
}

function activateDeactivateKeySkill(id) {
    loaderShow();
    $.get("/Dashboard/ActivateDeactivateKeySkill?keySkillMasterId=" + id, (result) => {
        LoaderHide();
        if (result.isSuccessful == true) {
            getKeySkillList($("#currentPageNo").val(), 10);
            displayToastMessages("success.svg", "success", "Success", result.message);
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
        }
    })
}

function getKeySkillList(pageNumber, pageSize) {
    loaderShow();
    $.get("/dashboard/GetKeySkills?pageNumber=" + pageNumber + "&pageSize=" + pageSize, (result) => {
        $("#keySkillList").html(result);
        LoaderHide();
    });
}
//--------------------------------------------------Key Skill Master methods end-------------------------------------------

//--------------------------------------------------Company Master methods start--------------------------------------------------

$(document).ready(() => {
    $("#companyMasterForm").hide();
})

$("#companyMasterAddBtn").on("click", () => {
    $("#companyMasterForm").show(2000);
    $("#companyMasterAddBtn").hide(2000);
})

$("#companyMasterCancelBtn").on("click", () => {
    $("#companyMasterForm").hide(2000);
    $("#companyMasterAddBtn").show(2000);
    $("#companyNameError").text("");
    $("#companyName").val("");
    $('#isCompanyActive').prop('checked', false);
    $("#companyId").val(0);
    $("#companyMasterSubmitBtn").text("Submit");
})

$("#companyMasterSubmitBtn").on("click", () => {
    var companyName = $("#companyName").val();
    if (companyName == "" || companyName == null) {
        $("#companyNameError").text("Company name is required");
        return;
    }

    var companyData = {
        companyName: companyName,
        isCompanyActive: $('#isCompanyActive').is(":checked"),
        companyId: $("#companyId").val()
    }

    loaderShow();
    $.post("/Dashboard/AddCompany", companyData, (result) => {
        LoaderHide();
        if (result.isSuccessful == true) {
            $("#companyName").val("");
            $('#isCompanyActive').prop('checked', false);
            $("#companyId").val(0);
            $("#companyMasterSubmitBtn").text("Submit");
            $("#companyMasterForm").hide(2000);
            $("#companyMasterAddBtn").show(2000);
            displayToastMessages("success.svg", "success", "Success", result.message);
            getCompanyList($("#currentPageNo").val(), 10);
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
        }
    })
})

$("#companyName").on("keypress", () => {
    $("#companyNameError").text("");
})

function deleteCompany(id) {

    cuteAlert({
        type: "question",     //"question",
        title: "Company Delete Confirmation",   //"Confirm Title",
        message: "Are you sure you want to delete this company?",   //"Confirm Message",
        img: "question.svg",
        confirmText: "Yes",    //"Okay",
        cancelText: "No"   //"Cancel"
    }).then((e) => {
        if (e == "confirm") {
            loaderShow();
            $.get("/Dashboard/DeleteCompany?companyMasterId=" + id, (result) => {
                LoaderHide();
                if (result.isSuccessful == true) {
                    displayToastMessages("success.svg", "success", "Success", result.message);
                    getCompanyList($("#currentPageNo").val(), 10);
                } else {
                    displayToastMessages("error.svg", "error", "Error", result.message);
                }
            })
        }
    })
}

function editCompany(companyId) {
    if ($(document).scrollTop() > 0) {
        $(document).scrollTop(-$(document).scrollTop());
    }

    loaderShow();
    $.get("/Dashboard/EditCompany?companyMasterId=" + companyId, (result) => {
        LoaderHide();
        if (result.isSuccessful == true) {
            displayToastMessages("success.svg", "success", "Success", result.message);
            $("#companyName").val(result.data.companyName);
            $('#isCompanyActive').prop('checked', result.data.isCompanyActive);
            $("#companyId").val(result.data.companyId);
            $("#companyMasterSubmitBtn").text("Update");
            $("#companyMasterForm").show(2000);
            $("#companyMasterAddBtn").hide(2000);
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
        }
    })
}

function activateDeactivateCompany(id) {
    loaderShow();
    $.get("/Dashboard/ActivateDeactivateCompany?companyMasterId=" + id, (result) => {
        LoaderHide();
        if (result.isSuccessful == true) {
            getCompanyList($("#currentPageNo").val(), 10);
            displayToastMessages("success.svg", "success", "Success", result.message);
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
        }
    })
}

function getCompanyList(pageNumber, pageSize) {
    loaderShow();
    $.get("/dashboard/GetCompanies?pageNumber=" + pageNumber + "&pageSize=" + pageSize, (result) => {
        $("#companyList").html(result);
        LoaderHide();
    });
}
//--------------------------------------------------Company Master methods end-------------------------------------------

//--------------------------------------------------Job Masster methods start--------------------------------------------
$(document).ready(() => {
    $("#newJobForm").hide();
})

$("#newJobAddBtn").on("click", () => {
    $("#newJobForm").show(2000);
    $("#newJobAddBtn").hide(2000);
    clearJobFields();
    getFormData();
})

var getFormData = () => {
    $.get("/Dashboard/GetFormData", (result) => {
        result.data.skillList.forEach(s => $("#jobSkills").append("<option value='" + s.value + "'>" + s.label + "<option>"));
        $("#jobSkills").select2({
            dropdownAutoWidth: true,
            width: '100%'
        });

        result.data.educationList.forEach(s => $("#education").append("<option value='" + s.value + "'>" + s.label + "<option>"));
        $("#education").select2({
            dropdownAutoWidth: true,
            width: '100%'
        });

        $("#jobCity").autocomplete({
            source: result.data.cityList,
            multiselect: true
        });

        $("#companyName").autocomplete({
            source: result.data.companyList,
            multiselect: true
        });
    })
}

$("#jobMasterCancelBtn").on("click", () => {
    $("#newJobForm").hide(2000);
    $("#newJobAddBtn").show(2000);
    clearJobFields();
})

var clearJobFields = () => {
    $("#jobTitle").val("");
    $("#jobTitleError").text("");

    $("#jobSkills").val("");
    $("#jobSkillsError").text("");

    $("#jobCity").val("");
    $("#jobCityError").text("");

    $("#jobExperiance").val("");
    $("#jobExperianceError").text("");

    $("#education").val("");
    $("#educationError").text("");

    $("#salary").val("");
    $("#salaryError").text("");

    $("#companyName").val("");
    $("#companyNameError").text("");

    $("#companyAddress").val("");
    $("#companyAddressError").text("");

    $("#jobDescription").val("");
    $("#jobDescriptionError").text("");

    $("#jdFile").val("");

    $("#jobId").val(0);
    $("#jobMasterSubmitBtn").text("Submit");

    $("#jdFileName").text("");
    //$("#jobSkills").select2('val', null); 
    //$("#education").select2('val', null); 

    $("#jobSkills").empty();
    $("#education").empty();
}

$("#jobMasterSubmitBtn").on("click", () => {
    var errorCount = 0;
    var jobTitle = $("#jobTitle").val();
    if (jobTitle == "" || jobTitle == null) {
        $("#jobTitleError").text("Job title is required");
        errorCount += errorCount + 1;
    }

    var jobSkills = $("#jobSkills").val();
    if (jobSkills == "" || jobSkills == null) {
        $("#jobSkillsError").text("Minimum one job skill is required");
        errorCount += errorCount + 1;
    }

    var jobCity = $("#jobCity").val();
    if (jobCity == "" || jobCity == null) {
        $("#jobCityError").text("City name is required");
        errorCount += errorCount + 1;
    }

    var jobExperince = $("#jobExperiance").val();
    if (jobExperince == "" || jobExperince == null) {
        $("#jobExperianceError").text("Experiance is required");
        errorCount += errorCount + 1;
    }

    var education = $("#education").val();
    if (education == "" || education == null) {
        $("#educationError").text("Minimum one education is required");
        errorCount += errorCount + 1;
    }

    var salary = $("#salary").val();
    if (salary == "" || salary == null) {
        $("#salaryError").text("Salary is required");
        errorCount += errorCount + 1;
    }

    var companyName = $("#companyName").val();
    if (companyName == "" || companyName == null) {
        $("#companyNameError").text("Company name is required");
        errorCount += errorCount + 1;
    }

    var companyAddress = $("#companyAddress").val();
    if (companyAddress == "" || companyAddress == null) {
        $("#companyAddressError").text("Company address is required");
        errorCount += errorCount + 1;
    }

    if ($("#jdFileName").text() == '') {
        var jobDescription = $("#jobDescription").val();
        if (jobDescription == "" || jobDescription == null) {
            var jdFile = $("#jdFile")[0].files[0];
            if (jdFile == undefined) {
                $("#jobDescriptionError").text("Job description or job description file is required");
                errorCount += errorCount + 1;
            } else {
                var accept = ".png;.jpg;.jpeg;.doc;.docx;.pdf";
                if (!accept.includes(jdFile.name.split('.')[1].toLowerCase())) {
                    $("#jdFileError").text("Only file with extention .png,.jpg,.jpeg,.doc,.docx,.pdf are allowed");
                    errorCount += errorCount + 1;
                }
                $("#jdFileName").text("- " + jdFile.name);
            }
        }
    }

    if (errorCount > 0) {
        return;
    }

    var jobData = {
        jobId: $("#jobId").val() == '' ? 0 : $("#jobId").val(),
        jobTitle: jobTitle,
        JobKeySkills: jobSkills.join(",").toString(),
        JobCity: jobCity,
        JobExperienceRequired: jobExperince,
        JobSalary: salary,
        JobEducationRequired: education.join(",").toString(),
        JobCompanyId: $("#companyId").val() == '' ? 0 : $("#companyId").val(),
        JobCompanyName: companyName,
        JobLocationAddress: companyAddress,
        JobDescriptionFileName: $("#jdFile")[0].files[0] != undefined ? $("#jdFile")[0].files[0].name : $("#jdFileName").text().split("-")[1],
        JobDescription: jobDescription
    };

    var formData = new FormData();
    formData.append("jobData", JSON.stringify(jobData));
    formData.append("jdFile", $("#jdFile")[0].files[0]);

    loaderShow();
    $.ajax({
        url: "/Dashboard/AddJob",
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (result) {
            LoaderHide();
            if (result.isSuccessful == true) {
                clearJobFields();
                $("#newJobForm").hide(2000);
                $("#newJobAddBtn").show(2000);
                displayToastMessages("success.svg", "success", "Success", result.message);
                getJobList($("#currentPageNo").val(), 10);
            } else {
                displayToastMessages("error.svg", "error", "Error", result.message);
            }
        },
        error: function (err) {
            console.log(err);
            displayToastMessages("error.svg", "error", "Error", result.message);
        }
    });
})

function clearTheErrorMsg(Id) {
    $("#" + Id).text("");
}

function getJobList(pageNumber, pageSize) {
    loaderShow();
    $.get("/dashboard/GetJobs?pageNumber=" + pageNumber + "&pageSize=" + pageSize, (result) => {
        $("#jobList").html(result);
        LoaderHide();
    });
}

function deleteJob(jobId) {

    cuteAlert({
        type: "question",     //"question",
        title: "Job Delete Confirmation",   //"Confirm Title",
        message: "Are you sure you want to delete this Job?",   //"Confirm Message",
        img: "question.svg",
        confirmText: "Yes",    //"Okay",
        cancelText: "No"   //"Cancel"
    }).then((e) => {
        if (e == "confirm") {
            loaderShow();
            $.get("/Dashboard/DeleteJob?jobMasterId=" + jobId, (result) => {
                LoaderHide();
                if (result.isSuccessful == true) {
                    displayToastMessages("success.svg", "success", "Success", result.message);
                    getJobList($("#currentPageNo").val(), 10);
                } else {
                    displayToastMessages("error.svg", "error", "Error", result.message);
                }
            })
        }
    })
}

function editJob(jobId) {
    if ($(document).scrollTop() > 0) {
        $(document).scrollTop(-$(document).scrollTop());
    }
    getFormData();
    loaderShow();
    $.get("/Dashboard/EditJob?jobMasterId=" + jobId, (result) => {
        LoaderHide();
        if (result.isSuccessful == true) {
            $("#jobMasterSubmitBtn").text("Update");
            $("#newJobForm").show(2000);
            $("#newJobAddBtn").hide(2000);

            displayToastMessages("success.svg", "success", "Success", result.message);
            $("#jobTitle").val(result.data.jobTitle);
            if (result.data.jobKeySkills.includes(",")) {
                //$('#jobSkills').val(result.data.jobKeySkills.split(','));
                var skills = result.data.jobKeySkills.split(',');
                $("#jobSkills").select2('val', skills);
            }
            else {
                var skill = Array(result.data.jobKeySkills)
                $("#jobSkills").select2('val', skill);
            }

            $("#jobExperiance").val(result.data.jobExperienceRequired);

            if (result.data.jobEducationRequired.includes(",")) {
                //$('#education').val(result.data.jobEducationRequired.split(','));
                var educations = result.data.jobEducationRequired.split(',')
                $("#education").select2('val', educations);
            }
            else {
                var education = Array(result.data.jobEducationRequired)
                $("#education").select2('val', education);
            }

            $("#salary").val(result.data.jobSalary);
            $("#jobCity").val(result.data.jobCity);
            $("#companyName").val(result.data.jobCompanyName);
            $("#companyId").val(result.data.jobCompanyId);
            $("#companyAddress").val(result.data.jobLocationAddress);
            $("#jobDescription").val(result.data.jobDescription);
            $("#jdFileName").text("- " + result.data.jobDescriptionFileName);
            $("#jobId").val(result.data.jobId)
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
            $("#newJobForm").hide(2000);
            $("#newJobAddBtn").show(2000);
            $("#jobMasterSubmitBtn").text("Submit");
        }
    })
}

var downloadJDFile = () => {
    window.location.href = "/Dashboard/DownloadJDFile?JDFileName=" + $("#jdFileName").text().split("-")[1];
}
//--------------------------------------------------Job Masster methods End--------------------------------------------

//--------------------------------------------------Happy Customer methods Start---------------------------------------
$(document).ready(() => {
    $("#newHappyCustomerForm").hide();
});

$("#newHappyCustomerAddBtn").on("click", () => {
    $("#newHappyCustomerForm").show(2000);
    $("#newHappyCustomerAddBtn").hide(2000);
    clearHappyCustomersFields();
});

$("#happyCustomerCancelBtn").on("click", () => {
    $("#newHappyCustomerForm").hide(2000);
    $("#newHappyCustomerAddBtn").show(2000);
    clearHappyCustomersFields();
})

var clearHappyCustomersFields = () => {
    $("#happyCustomerCompanyName").val("");
    $("#happyCustomerCompanyNameError").text("");

    $("#happyCustomerComment").val("");
    $("#happyCustomerCommentError").text("");

    $("#hcLogoFile").val("");
    $("#hcLogoFileError").text("");

    $("#happyCustomerId").val(0);
    $("#happyCustomerSubmitBtn").text("Submit");

    $("#hcLogoFileName").text("");
    $(".imgPreview").removeAttr("src")
}

$("#happyCustomerSubmitBtn").on("click", () => {
    var errorCount = 0;
    var happyCustomerName = $("#happyCustomerCompanyName").val();
    if (happyCustomerName == "" || happyCustomerName == null) {
        $("#happyCustomerCompanyNameError").text("Happy customer's company name is required");
        errorCount += errorCount + 1;
    }

    var happyCustomerComment = $("#happyCustomerComment").val();
    if (happyCustomerComment == "" || happyCustomerComment == null) {
        $("#happyCustomerCommentError").text("Happy customer's comment is required");
        errorCount += errorCount + 1;
    }

    if ($("#hcLogoFileName").text() == '') {
        var hcLogoFile = $("#hcLogoFile")[0].files[0];
        if (hcLogoFile == undefined) {
            $("#hcLogoFileError").text("Happy customer's company logo is required");
            errorCount += errorCount + 1;
        } else {
            var accept = ".gif;.png;.jpg;.jpeg;";
            if (!accept.includes(hcLogoFile.name.split('.')[1].toLowerCase())) {
                $("#hcLogoFileError").text("Only file with extention .png,.jpg,.jpeg,.gif are allowed");
                errorCount += errorCount + 1;
            }            
        }
    } else {
        $("#hcLogoFileName").text("- " + hcLogoFile.name);
    }

    if (errorCount > 0) {
        return;
    }

var happyCustomerData = {
    happyCustomerId: $("#happyCustomerId").val() == '' ? 0 : $("#happyCustomerId").val(),
    happyCustomerCompanyName: $("#happyCustomerCompanyName").val(),
    happyCustomerComment: $("#happyCustomerComment").val(),
    happyCustomerCompanyLogoName: $("#hcLogoFile")[0].files[0] != undefined ? $("#hcLogoFile")[0].files[0].name : $("#hcLogoFile").text().split("-")
};

var formData = new FormData();
    formData.append("happyCustomerData", JSON.stringify(happyCustomerData));
    formData.append("hcLogoFile", $("#hcLogoFile")[0].files[0]);

loaderShow();
$.ajax({
    url: "/Dashboard/AddHappyCustomer",
    type: 'POST',
    data: formData,
    contentType: false,
    processData: false,
    success: function (result) {
        LoaderHide();
        if (result.isSuccessful == true) {
            clearJobFields();
            $("#newHappyCustomerForm").hide(2000);
            $("#newHappyCustomerAddBtn").show(2000);
            displayToastMessages("success.svg", "success", "Success", result.message);
            getHCList($("#currentPageNo").val(), 10);
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
        }
    },
    error: function (err) {
        console.log(err);
        displayToastMessages("error.svg", "error", "Error", result.message);
    }
});
})

$("#hcLogoFile").on("change", () => {

    var format = /[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/;

    if (format.test($("#hcLogoFile")[0].files[0].name)) {
        $(".imgPreview").attr("src", URL.createObjectURL($("#hcLogoFile")[0].files[0]))
        
    } else {
        $("#hcLogoFileError").text("Invalid file name, please update the file name by omitting *,%,&,!,^,# symbols");
    }

    
})

function getHCList(pageNumber, pageSize) {
    loaderShow();
    $.get("/dashboard/GetHappyCustomers?pageNumber=" + pageNumber + "&pageSize=" + pageSize, (result) => {
        $("#hcList").html(result);
        LoaderHide();
    });
}

function editHappyCustomer(hcId) {
    if ($(document).scrollTop() > 0) {
        $(document).scrollTop(-$(document).scrollTop());
    }
    
    loaderShow();
    $.get("/Dashboard/EditHappyCustomer?happyCustomerId=" + hcId, (result) => {
        LoaderHide();
        if (result.isSuccessful == true) {
            $("#happyCustomerSubmitBtn").text("Update");
            $("#newHappyCustomerForm").show(2000);
            $("#newHappyCustomerAddBtn").hide(2000);

            displayToastMessages("success.svg", "success", "Success", result.message);
            $("#happyCustomerCompanyName").val(result.data.happyCustomerCompanyName);
            $("#hcLogoFileName").text(result.data.happyCustomerCompanyLogoName);
            $("#happyCustomerComment").val(result.data.happyCustomerComment);
            $(".imgPreview").attr("src", window.location.origin + "/HappyCustomers/" + result.data.happyCustomerCompanyLogoName);
            $("#happyCustomerId").val(result.data.happyCustomerId)
        } else {
            displayToastMessages("error.svg", "error", "Error", result.message);
            $("#newHappyCustomerForm").hide(2000);
            $("#newHappyCustomerAddBtn").show(2000);
            $("#happyCustomerSubmitBtn").text("Submit");
        }
    })
}

var downloadHCLogo = () => {
    window.location.href = "/Dashboard/DownloadLogoFile?logoFileName=\'" + $("#hcLogoFileName").text() + "\'";
}


//--------------------------------------------------Happy Customer methods End-----------------------------------------

