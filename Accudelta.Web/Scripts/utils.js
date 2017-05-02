"use strict";
var $ = require('jquery');
exports.getFunds = "/Home/GetFunds";
var Utils = (function () {
    function Utils() {
    }
    Utils.getDataGrid = function (url, data, success, error) {
        console.log(url);
        var urlFull = this.home() + url;
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: urlFull,
            timeout: 10000,
            dataType: "json",
            data: JSON.stringify(data),
            success: function (s, e) {
                this.showLoading(false);
                success(s, e);
            },
            error: function (s, e) {
                this.showLoading(false);
                error(s, e);
            }
        });
    };
    Utils.showLoading = function (show) {
        if (show)
            $("#spinner-container").show();
        else
            $("#spinner-container").hide();
    };
    Utils.home = function () {
        return $("#BaseUrl").data("baseurl").trim();
    };
    return Utils;
}());
exports.Utils = Utils;
//# sourceMappingURL=utils.js.map