  $(document).ready(function () {
    loadData();
});
 
function loadData() {
    $.ajax({
        url: "/UserProfile/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Village + '</td>';
                html += '<td>' + item.City + '</td>';
                html += '<td>' + item.Country + '</td>';
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
       
        Village: $('#Village').val(),
        City: $('#City').val(),
        Country: $('#Country').val(),
        UserId: $('#UserId').val(),
    };
    $.ajax({
        url: "/UserProfile/Add",
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
    $('#Village').css('border-color', 'lightgrey');
    $('#City').css('border-color', 'lightgrey');
    $('#Country').css('border-color', 'lightgrey');
    $.ajax({
        url: "/UserProfile/GetbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Id').val(result.Id);
            $('#Village').val(result.Village);
            $('#City').val(result.City);
            $('#Country').val(result.Country);
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
        Village: $('#Village').val(),
        City: $('#City').val(),
        Country: $('#Country').val(),
        UserId:$('#UserId').val(),
    };
    $.ajax({
        url: "/UserProfile/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#Id').val("");
            $('#Village').val("");
            $('#City').val("");
            $('#Country').val("");
            $('#UserId').val();
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
            url: "/UserProfile/Delete/" + ID,
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
    $('#Village').val("");
    $('#City').val("");
    $('#Country').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Village').css('border-color', 'lightgrey');
    $('#City').css('border-color', 'lightgrey');
    $('#Country').css('border-color', 'lightgrey');
  
}

function validate() {
    var isValid = true;   
    
     $('#Village').css('border-color', 'lightblue');
    if ($('#City').val().trim() == "") {
        $('#City').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#City').css('border-color', 'lightblue');
    }
    $('#Country').css('border-color', 'lightblue');
   
}  