import * as React from "react";
import * as $ from 'jquery';
import * as ReactPaginate from 'react-paginate';
import {Link} from 'react-router';

import Utils = require('../utils');

interface IData {
    data?: any[],
    pageCount?: number,
    offset?: number,
    perPage?: number
}
interface ITableData {
    url: string
}
interface ITableRowsData {
    data: any[]
}
interface ITableRowsState {
    redirect: boolean
}
interface Nothing { }

export class Table extends React.Component<ITableData, IData> {

    public state: IData;

    constructor(props: any) {
        super(props);
        this.state = {
            data: [],
            offset: 0,
            perPage: 10,
            pageCount: 10            
        };

        this.pageChanged = this.pageChanged.bind(this);
    }    
    
    populateData() {     
        
        var urlFull = Utils.Utils.home() + this.props.url;
        Utils.Utils.showLoading(true);
        $.ajax({
                url: urlFull,
                data: { offset: this.state.offset, limit: this.state.perPage },
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
        this.populateData();
    }

    pageChanged(data: any) {
        let selected = data.selected;
        let offset = Math.ceil(selected * this.state.perPage);

        this.setState({ offset: offset }, () => {
            this.populateData();
        });
    }

    render() {
            return (
                <div>
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
                        containerClassName={"pagination"}
                        activeClassName={"active"} />                    
                </div>
            );
        }
}

export class TableRows extends React.Component<ITableRowsData, Nothing> {

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