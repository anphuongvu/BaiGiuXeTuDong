const ctx = document.getElementById('lineChart').getContext('2d');
const myChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: ['Tháng 1','Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'],
        datasets: [{
            label: 'Lượt gửi',
            data: [1231, 1200, 1513, 992, 1123, 1313, 1034, 1213, 1316, 2342, 1245, 1315],
            backgroundColor: [
                'rgb(166, 169, 255)'
            ],
            borderColor: [
                'rgb(84, 204, 255)'
            ],
            borderWidth: 1
        }]
    },
    options: {
        responsive: true
    }
});