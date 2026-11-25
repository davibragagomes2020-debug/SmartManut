document.addEventListener('DOMContentLoaded', async () => {
    await carregarTabela();
    document.getElementById('formDados').addEventListener('submit', async (e) => {
        e.preventDefault();
        const payload = {
            nomeEmpresa: document.getElementById('nomeEmpresa').value,
            contato: document.getElementById('contato').value,
            prazoEntregaDias: 7
        };
        const res = await fetch('/api/fornecedores', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(payload) });
        if (res.ok) { alert('Fornecedor salvo!'); e.target.reset(); await carregarTabela(); } else alert('Erro: ' + await res.text());
    });
});
async function carregarTabela() {
    const res = await fetch('/api/fornecedores');
    if (res.ok) {
        const dados = await res.json();
        document.getElementById('tabelaDados').innerHTML = dados.map(x => `<tr><td>${x.id ?? x.Id}</td><td>${x.nomeEmpresa ?? x.NomeEmpresa}</td><td>${x.contato ?? x.Contato}</td></tr>`).join('');
    }
}
