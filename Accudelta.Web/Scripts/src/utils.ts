import * as $ from 'jquery';

export var getFunds: string = "/Home/GetFunds";     

class Utils {
    
  public static getDataGrid(url: string, data: any, success: any, error?: any) {
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
    }

    public static showLoading(show: boolean) {
        if (show)
            $("#spinner-container").show();
        else
            $("#spinner-container").hide();
    }

    public static home() {
        return $("#BaseUrl").data("baseurl").trim();
    }
}
export { Utils };