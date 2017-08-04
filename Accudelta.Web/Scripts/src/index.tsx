import * as React from "react";
import * as ReactDOM from "react-dom";
import { Table } from "./components/table";


ReactDOM.render(
    <div>
        <h1>Funds</h1>
        <Table url="/Home/GetFunds" />
    </div>,
    document.getElementById("body")
);