import React, { Component } from 'react';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend,
  } from 'chart.js';
import { Line } from 'react-chartjs-2';
ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
  );

  export const options = {
    responsive: true,
    plugins: {
      legend: {
        position: 'top',
      },
      title: {
        display: true,
        text: 'Chart.js Line Chart',
      },
    },
  };
  
 

class WeeklyPerformanceChart extends Component {
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
     
    const labels = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];

var labelSet = records.map((record) => record.date);
const data = {
    labels,
    datasets: [
      {
        label: 'Dataset 1',
        data: records.map((record) => record.performance),
        borderColor: 'rgb(255, 99, 132)',
        backgroundColor: 'rgba(255, 99, 132, 0.5)',
      }
    ],
  };

     return <div width="10%"><Line options={options} data={data} width={"1000%"} /> </div>;
  }

}

export default WeeklyPerformanceChart ;