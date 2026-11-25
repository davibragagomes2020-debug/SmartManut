document.addEventListener('DOMContentLoaded', async () => {
    await carregarSelect('/api/ordensservico', 'ordemServicoId', os => os.id ?? os.Id, os => `OS #${os.id ?? os.Id} - ${os.descricaoProblema ?? os.DescricaoProblema}`);
    await carregarTabela();
    document.getElementById('formDados').addEventListener('submit', async (e) => {
        e.preventDefault();
        const payload = {
            ordemServicoId: parseInt(document.getElementById('ordemServicoId').value),
            quantidadeAplicada: parseInt(document.getElementById('quantidadeAplicada').value),
            relatorioTecnico: document.getElementById('relatorioTecnico').value,
            tempoGastoHoras: 2.5
        };
        const res = await fetch('/api/historicomanutencoes', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(payload) });
        if (res.ok) { alert('Histórico registado!'); e.target.reset(); await carregarTabela(); } else alert('Erro: ' + await res.text());
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
    const res = await fetch('/api/historicomanutencoes');
    if (res.ok) {
        const dados = await res.json();
        document.getElementById('tabelaDados').innerHTML = dados.map(x => `<tr><td>${x.id ?? x.Id}</td><td>${x.ordemServicoId ?? x.OrdemServicoId}</td><td>${x.relatorioTecnico ?? x.RelatorioTecnico}</td><td>${x.quantidadeAplicada ?? x.QuantidadeAplicada}</td><td>${x.dataRegistro ?? x.DataRegistro}</td></tr>`).join('');
    }
}
