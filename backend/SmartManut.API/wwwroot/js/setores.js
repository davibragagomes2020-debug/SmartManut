document.addEventListener('DOMContentLoaded', async () => {
    await carregarTabela();
    document.getElementById('formDados').addEventListener('submit', async (e) => {
        e.preventDefault();
        const payload = {
            nomeSetor: document.getElementById('nomeSetor').value,
            localizacao: document.getElementById('localizacao').value
        };
        const res = await fetch('/api/setores', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(payload) });
        if (res.ok) { alert('Setor salvo!'); e.target.reset(); await carregarTabela(); } else alert('Erro: ' + await res.text());
    });
});
async function carregarTabela() {
    const res = await fetch('/api/setores');
    if (res.ok) {
        const dados = await res.json();
        document.getElementById('tabelaDados').innerHTML = dados.map(x => `<tr><td>${x.id ?? x.Id}</td><td>${x.nomeSetor ?? x.NomeSetor}</td><td>${x.localizacao ?? x.Localizacao}</td></tr>`).join('');
    }
}
