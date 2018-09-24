$(document).ready(function () {
    loadData();
});

function loadData() {
    $.ajax({
        url: "/Course/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) { 
            var html = '';
            $.each(result, function (key, item) {
                var MyDate_String_Value = item.DateCreateCourse;
                var value = new Date
                    (
                    parseInt(MyDate_String_Value.replace(/(^.*\()|([+-].*$)/g, ''))
                    );
                var dat =  value.getDate() +
                    "/" +
                    (value.getMonth() +1) +
                    "/" +
                    value.getFullYear();
                html += '<tr>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + dat+ '</td>';  
                html += '<td><a href="#" onclick="return getbyID(' + item.Id + ')">Edit</a> | <a href="#" onclick="Delele(' + item.Id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var empObj = {

        NameCourse: $('#NameCourse').val(),
        DateCreated: $('#DateCreated').val(),
        TeacherId: $('#TeacherId').val(),
    };
    $.ajax({
        url: "/Course/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function getbyID(ID) {
    $('#NameCourse').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Course/GetbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Id').val(result.Id);
            $('#NameCourse').val(result.NameCourse);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}


function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        Id: $('#Id').val(),
        NameCourse: $('#NameCourse').val(),
        DateCreated: $('#DateCreated').val(),
        TeacherId: $('#TeacherId').val(),
    };
    $.ajax({
        url: "/Course/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#Id').val("");
            $('#NameCourse').val("");
            $('#DateCreated').val("");
            $('#TeacherId').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Course/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
function clearTextBox() {
    $('#NameCourse').val("");
    $('#DataCreated').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#NameCourse').css('border-color', 'lightgrey');
    $('#DataCreated').css('border-color', 'lightgrey');


}

function validate() {
    var isValid = true;

    $('#NameCourse').css('border-color', 'lightblue');
    if ($('#NameCourse').val().trim() == "") {
        $('#NameCourse').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#NameCourse').css('border-color', 'lightblue');
    }
  
    
       $('#DataCreated').css('border-color', 'lightblue');
    
   
}  