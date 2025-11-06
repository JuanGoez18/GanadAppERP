window.crearGraficoUsuarios = () => {
    const ctx = document.getElementById('graficoUsuarios');
    if (!ctx) return; // Evita error si no existe el canvas

    new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun'],
            datasets: [{
                label: 'Usuarios',
                data: [50, 80, 120, 140, 170, 200],
                borderColor: '#f97316',
                tension: 0.3,
                fill: false
            }]
        },
        options: {
            responsive: true,
            plugins: { legend: { display: false } },
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
};