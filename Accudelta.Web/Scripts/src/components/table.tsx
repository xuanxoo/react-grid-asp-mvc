import * as React from "react";
import * as $ from 'jquery';
import * as ReactPaginate from 'react-paginate';
import {Link} from 'react-router';

import Utils = require('../utils');

interface ITableStats {
    data?: any[],
    pageCount?: number,
    offset?: number,
    perPage?: number,
    query?: string,
    currentPage? : number
}
interface ITableProps {
    url: string
}
interface ITableRowsProps {
    data: any[]
}
interface ITableRowsState {
    redirect: boolean
}
interface ITableSearchProps {
    onSearch: (query: string) => void;
}
interface ITableSearchState {
    query:string
}
interface Nothing { }

export class Table extends React.Component<ITableProps, ITableStats> {

    public state: ITableStats;

    constructor(props: any) {
        super(props);
        this.state = {
            data: [],
            offset: 0,
            perPage: 10,
            pageCount: 10,
            query: "",
            currentPage: 0       
        };

        this.pageChanged = this.pageChanged.bind(this);
        this.search = this.search.bind(this);
    }    
    
    populateData(query:string) {     
        
        var urlFull = Utils.Utils.home() + this.props.url;
        Utils.Utils.showLoading(true);
        $.ajax({
                url: urlFull,
                data: { offset: this.state.offset, limit: this.state.perPage, query: query },
                type: 'GET',
                success: function (data: any) {
                    this.setState({
                        data: data.Funds,
                        pageCount: Math.ceil(data.TotalCount / this.state.perPage)
                    })
                    Utils.Utils.showLoading(false);
                }.bind(this),
                error: function (err: any) {
                    Utils.Utils.showLoading(false);
                    alert('Error');
                }.bind(this)
            });
    }
    
    componentDidMount() {
        this.populateData("");
    }

    pageChanged(data: any) {
        let selected = data.selected;
        let offset = Math.ceil(selected * this.state.perPage);

        this.setState({
            offset: offset,
            currentPage: data.selected
        }, () => {
            this.populateData(this.state.query);
        });
    }

    search(query: string) {
        this.setState({
            offset: 0,
            query: query,
            currentPage: 0
        }, () => {
            this.populateData(query);
        });
    }

    render() {
            return (
                <div>
                    <TableSearch onSearch={this.search} />
                    <table className="table table-responsive table-bordered">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Fund Name</th>
                                <th>Fund Description</th>
                                <th>Last fund value</th>
                                <th>Last fund date value</th>
                            </tr>
                        </thead>
                        <TableRows data={this.state.data} /> 
                    </table>
                    <ReactPaginate previousLabel={"previous"}
                        nextLabel={"next"}
                        breakLabel={<a href="">...</a>}
                        breakClassName={"break-me"}
                        pageCount={this.state.pageCount}
                        marginPagesDisplayed={2}
                        pageRangeDisplayed={5}
                        onPageChange={this.pageChanged}
                        forcePage={this.state.currentPage}
                        containerClassName={"pagination"}
                        activeClassName={"active"} />                    
                </div>
            );
        }
}

class TableRows extends React.Component<ITableRowsProps, Nothing> {

    constructor(props: any) {
        super(props);
    }

    render() {
       
        const rows = this.props.data.map(row =>
            <tr key={row.Id} >
                <td>{row.Id}</td>
                <td>{row.Name}</td>
                <td>{row.Description}</td>
                <td>{row.LastValue}</td>
                <td>{row.DateValue}</td>
            </tr>

        );

        return (
            <tbody>
                { rows }
              
            </tbody>
        );
    }
}

class TableSearch extends React.Component<ITableSearchProps, ITableSearchState> {

    public props: ITableSearchProps;

    constructor(props: any) {
        super(props);
        this.state = { query: "" }
        this.onSearch = this.onSearch.bind(this);
    }

    onSearch() {
        this.props.onSearch($("#inputSearch").val());
    }

    render() {        
        return (
            <div className="form-horizontal" id="searchForm">
                <div className="form-group"> 
                    <div className="col-md-4">
                        <label className="col-md-2 control-label">Name</label>
                        <div className="col-md-10"><input name="inputSearch" type="text" className="form-control" id="inputSearch" /></div>
                    </div>                        
                    <div className="col-md-2 inline">
                        <button id="btnSearch" className="btn btn-default" onClick={this.onSearch}>Search</button>
                    </div>
                </div>
            </div>
           );
        }
}