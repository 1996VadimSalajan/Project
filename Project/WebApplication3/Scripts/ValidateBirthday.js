$(function () {
    $.validator.addMethod(
        "validbirthday", //should be same from  ValidationType = "positivenumber"
        ///<summary>Method adding positive number validation.</summmary>
        function (value, element) {
            try {
                if ($(element).is('disabled'))
                    return true;
                if ($(element).is('[data-val]')) {
                    if (($(element).val())!=value)
                        return false;
                    return true
                }
            } catch (e) {
                return false;
            }
            return false;
        },
        "");
    $.validator.unobtrusive.adapters.addBool("validbirthday");
}(jQuery));