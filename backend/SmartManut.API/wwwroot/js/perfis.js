document.addEventListener('DOMContentLoaded', async () => {
    await carregarTabela();
    document.getElementById('formDados').addEventListener('submit', async (e) => {
        e.preventDefault();
        const payload = {
            nomeCargo: document.getElementById('nomeCargo').value,
            permissoes: document.getElementById('permissoes').value
        };
        const res = await fetch('/api/perfis', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(payload) });
        if (res.ok) { alert('Perfil salvo!'); e.target.reset(); await carregarTabela(); } else alert('Erro: ' + await res.text());
    });
});
async function carregarTabela() {
    const res = await fetch('/api/perfis');
    if (res.ok) {
        const dados = await res.json();
        document.getElementById('tabelaDados').innerHTML = dados.map(x => `<tr><td>${x.id ?? x.Id}</td><td>${x.nomeCargo ?? x.NomeCargo}</td><td>${x.permissoes ?? x.Permissoes}</td></tr>`).join('');
    }
}
