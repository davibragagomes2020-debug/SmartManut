document.addEventListener('DOMContentLoaded', async () => {
    await carregarSelect('/api/setores', 'setorId', s => s.id ?? s.Id, s => s.nomeSetor ?? s.NomeSetor);
    await carregarTabela();
    document.getElementById('formDados').addEventListener('submit', async (e) => {
        e.preventDefault();
        const payload = {
            codigo: document.getElementById('codigo').value,
            nome: document.getElementById('nome').value,
            status: "Ativo",
            setorId: parseInt(document.getElementById('setorId').value)
        };
        const res = await fetch('/api/equipamentos', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(payload) });
        if (res.ok) { alert('Equipamento salvo!'); e.target.reset(); await carregarTabela(); } else alert('Erro: ' + await res.text());
    });
});
async function carregarSelect(url, selectId, idFunc, textFunc) {
    const res = await fetch(url);
    if (res.ok) {
        const data = await res.json();
        document.getElementById(selectId).innerHTML = data.map(x => `<option value="${idFunc(x)}">${textFunc(x)}</option>`).join('');
    }
}
async function carregarTabela() {
    const res = await fetch('/api/equipamentos');
    if (res.ok) {
        const dados = await res.json();
        document.getElementById('tabelaDados').innerHTML = dados.map(x => `<tr><td>${x.id ?? x.Id}</td><td>${x.codigo ?? x.Codigo}</td><td>${x.nome ?? x.Nome}</td><td>${x.status ?? x.Status}</td><td>${x.setorId ?? x.SetorId}</td></tr>`).join('');
    }
}
