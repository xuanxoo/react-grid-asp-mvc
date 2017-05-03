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
            pageCount: 10,
            query: ""
        };
        this.pageChanged = this.pageChanged.bind(this);
        this.search = this.search.bind(this);
    }
    Table.prototype.populateData = function (query) {
        var urlFull = Utils.Utils.home() + this.props.url;
        Utils.Utils.showLoading(true);
        $.ajax({
            url: urlFull,
            data: { offset: this.state.offset, limit: this.state.perPage, query: query },
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
        this.populateData("");
    };
    Table.prototype.pageChanged = function (data) {
        var _this = this;
        var selected = data.selected;
        var offset = Math.ceil(selected * this.state.perPage);
        this.setState({ offset: offset }, function () {
            _this.populateData(_this.state.query);
        });
    };
    Table.prototype.search = function (query) {
        this.populateData(query);
    };
    Table.prototype.render = function () {
        return (React.createElement("div", null, React.createElement(TableSearch, {onSearch: this.search}), React.createElement("table", {className: "table table-responsive table-bordered"}, React.createElement("thead", null, React.createElement("tr", null, React.createElement("th", null, "Id"), React.createElement("th", null, "Fund Name"), React.createElement("th", null, "Fund Description"), React.createElement("th", null, "Last fund value"), React.createElement("th", null, "Last fund date value"))), React.createElement(TableRows, {data: this.state.data})), React.createElement(ReactPaginate, {previousLabel: "previous", nextLabel: "next", breakLabel: React.createElement("a", {href: ""}, "..."), breakClassName: "break-me", pageCount: this.state.pageCount, marginPagesDisplayed: 2, pageRangeDisplayed: 5, onPageChange: this.pageChanged, containerClassName: "pagination", activeClassName: "active"})));
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
var TableSearch = (function (_super) {
    __extends(TableSearch, _super);
    function TableSearch(props) {
        _super.call(this, props);
        this.state = { query: "" };
        this.onSearch = this.onSearch.bind(this);
    }
    TableSearch.prototype.onSearch = function () {
        this.props.onSearch($("#inputSearch").val());
    };
    TableSearch.prototype.render = function () {
        return (React.createElement("div", {className: "form-horizontal", id: "searchForm"}, React.createElement("div", {className: "form-group"}, React.createElement("div", {className: "col-md-4"}, React.createElement("input", {name: "inputSearch", type: "text", className: "form-control", id: "inputSearch"})), React.createElement("div", {className: "col-md-2 inline"}, React.createElement("button", {id: "btnSearch", className: "btn btn-default", onClick: this.onSearch}, "Search")))));
    };
    return TableSearch;
}(React.Component));
exports.TableSearch = TableSearch;
//# sourceMappingURL=table.js.map