import { Component, OnInit } from '@angular/core';
import { LoggedInUser } from '../../interface/logged.user.interface';
import { AccountService } from '../../services/account.service';
import { Chart } from 'chart.js';
import { DashboardService } from '../../services/dashboard.service';

@Component({
  selector: 'dashboard-home',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  companyChart = [];
  categoryChart = [];
  showSpinner: boolean = true;

  constructor(private user: AccountService, private dashboardService: DashboardService) { }

  ngOnInit() {
    this.user.getUserDetails();

    //get chart data

    this.getCompanyData();

    this.getCategoryData();

  }

  getCompanyData() {
    this.showSpinner = false;
    this.dashboardService.getCompanyDetails().subscribe((res: any) => {
      if (res.data != null) {
        let chartLabels = [];
        let chartData = [];

        res.data.forEach(data => {
          chartLabels.push(data.companyName);
          chartData.push(data.companyItems);
        });

        this.companyChart = new Chart('companyChart',
          {
            type: 'line',
            data: {
              labels: chartLabels,
              datasets: [
                {
                  label: '# of Items',
                  data: chartData,
                  backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                  ],
                  borderColor: [
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                  ],
                  borderWidth: 1
                }
              ]
            }
          });
      } else {
        this.companyChart = null;
      }
    });
  }

  getCategoryData() {
    this.dashboardService.getCategoryDetails().subscribe((res: any) => {
      if (res.data != null) {
        let chartLabels = [];
        let chartData = [];

        res.data.forEach(data => {
          chartLabels.push(data.categoryName);
          chartData.push(data.items);
        });

        this.categoryChart = new Chart('categoryChart', {
          type: 'line',
          data: {
            labels: chartLabels,
            datasets: [{
              label: '# of Items',
              data: chartData,
              backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)'
              ],
              borderColor: [
                'rgba(255,99,132,1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
              ],
              borderWidth: 1
            }],
            options: {
              elements: {
                line: {
                  tension: 0, // disables bezier curves
                }
              }
            }
          }
        });
      }
    });
  }
}
