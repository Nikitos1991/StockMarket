import React, { Component } from 'react';
class WeeklyPerformanceReport extends Component {
    constructor(props) {
        super(props);
   
        this.state = {
            items: [],
            DataisLoaded: false
        };
    }
  
  generateReportData(){
    fetch('http://localhost:8000/stock-reports/weekly-performance?symbol=AAPL')
    .then(response => response.json())
    .then(records => {
        this.setState({
            records: records,
            DataisLoaded: true
        })
    })
    .catch(error => console.log(error))
  }
  
  render() {
    const { DataisLoaded, records } = this.state;
    if (!DataisLoaded) 
    { 
         this.generateReportData() ;
         return <div>
            <h1> Pleses wait some time.... </h1> </div> ;
    }
     
     return(<div className = "App">
     <h1> Performance comparison report </h1>  
     <table>
  <tr>
    <th>Date</th>
    <th>Price</th>
    <th>performance</th>
  </tr>
     {
         records.map((record) => ( 
          <tr key="{record}">
            <td>{ record.date }</td>
            <td>{ record.price }</td>
            <td>{ record.performance }%</td>
          </tr>
         ))
     }
     </table>
 </div>);
  }

}

export default WeeklyPerformanceReport;