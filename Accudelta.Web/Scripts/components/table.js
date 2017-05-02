"use strict";
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var React = require("react");
var $ = require('jquery');
var ReactPaginate = require('react-paginate');
var Utils = require('../utils');
var Table = (function (_super) {
    __extends(Table, _super);
    function Table(props) {
        _super.call(this, props);
        this.state = {
            data: [],
            offset: 0,
            perPage: 10,
            pageCount: 10
        };
        this.pageChanged = this.pageChanged.bind(this);
    }
    Table.prototype.populateData = function () {
        var urlFull = Utils.Utils.home() + this.props.url;
        Utils.Utils.showLoading(true);
        $.ajax({
            url: urlFull,
            data: { offset: this.state.offset, limit: this.state.perPage },
            type: 'GET',
            success: function (data) {
                this.setState({
                    data: data.Funds,
                    pageCount: Math.ceil(data.TotalCount / this.state.perPage)
                });
                Utils.Utils.showLoading(false);
            }.bind(this),
            error: function (err) {
                Utils.Utils.showLoading(false);
                alert('Error');
            }.bind(this)
        });
    };
    Table.prototype.componentDidMount = function () {
        this.populateData();
    };
    Table.prototype.pageChanged = function (data) {
        var _this = this;
        var selected = data.selected;
        var offset = Math.ceil(selected * this.state.perPage);
        this.setState({ offset: offset }, function () {
            _this.populateData();
        });
    };
    Table.prototype.render = function () {
        return (React.createElement("div", null, React.createElement("table", {className: "table table-responsive table-bordered"}, React.createElement("thead", null, React.createElement("tr", null, React.createElement("th", null, "Id"), React.createElement("th", null, "Fund Name"), React.createElement("th", null, "Fund Description"), React.createElement("th", null, "Last fund value"), React.createElement("th", null, "Last fund date value"))), React.createElement(TableRows, {data: this.state.data})), React.createElement(ReactPaginate, {previousLabel: "previous", nextLabel: "next", breakLabel: React.createElement("a", {href: ""}, "..."), breakClassName: "break-me", pageCount: this.state.pageCount, marginPagesDisplayed: 2, pageRangeDisplayed: 5, onPageChange: this.pageChanged, containerClassName: "pagination", activeClassName: "active"})));
    };
    return Table;
}(React.Component));
exports.Table = Table;
var TableRows = (function (_super) {
    __extends(TableRows, _super);
    function TableRows(props) {
        _super.call(this, props);
    }
    TableRows.prototype.render = function () {
        var rows = this.props.data.map(function (row) {
            return React.createElement("tr", {key: row.Id}, React.createElement("td", null, row.Id), React.createElement("td", null, row.Name), React.createElement("td", null, row.Description), React.createElement("td", null, row.LastValue), React.createElement("td", null, row.DateValue));
        });
        return (React.createElement("tbody", null, rows));
    };
    return TableRows;
}(React.Component));
exports.TableRows = TableRows;
//# sourceMappingURL=table.js.map