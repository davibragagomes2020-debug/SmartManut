document.addEventListener('DOMContentLoaded', async () => {
    await carregarSelect('/api/equipamentos', 'equipamentoId', eq => eq.id ?? eq.Id, eq => `${eq.codigo ?? eq.Codigo} - ${eq.nome ?? eq.Nome}`);
    await carregarTabela();
    document.getElementById('formDados').addEventListener('submit', async (e) => {
        e.preventDefault();
        const payload = {
            tipo: document.getElementById('tipo').value,
            prioridade: document.getElementById('prioridade').value,
            status: "Aberta",
            descricaoProblema: document.getElementById('descricaoProblema').value,
            equipamentoId: parseInt(document.getElementById('equipamentoId').value),
            solicitanteId: 1
        };
        const res = await fetch('/api/ordensservico', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(payload) });
        if (res.ok) { alert('Ordem salva!'); e.target.reset(); await carregarTabela(); } else alert('Erro: ' + await res.text());
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
    const res = await fetch('/api/ordensservico');
    if (res.ok) {
        const dados = await res.json();
        document.getElementById('tabelaDados').innerHTML = dados.map(x => `<tr><td>${x.id ?? x.Id}</td><td>${x.tipo ?? x.Tipo}</td><td>${x.descricaoProblema ?? x.DescricaoProblema}</td><td>${x.status ?? x.Status}</td><td>${x.prioridade ?? x.Prioridade}</td><td>${x.equipamentoId ?? x.EquipamentoId}</td></tr>`).join('');
    }
}
