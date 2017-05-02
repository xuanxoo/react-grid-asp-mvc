"use strict";
var React = require("react");
var ReactDOM = require("react-dom");
var table_1 = require("./components/table");
ReactDOM.render(React.createElement("div", null, React.createElement("h1", null, "Funds"), React.createElement(table_1.Table, {url: "/Home/GetFunds"})), document.getElementById("body"));
//# sourceMappingURL=index.js.map