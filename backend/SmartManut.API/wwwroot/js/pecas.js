document.addEventListener('DOMContentLoaded', async () => {
    await carregarSelect('/api/fornecedores', 'fornecedorId', f => f.id ?? f.Id, f => f.nomeEmpresa ?? f.NomeEmpresa);
    await carregarTabela();
    document.getElementById('formDados').addEventListener('submit', async (e) => {
        e.preventDefault();
        const payload = {
            codigoSku: document.getElementById('codigoSku').value,
            nome: document.getElementById('nome').value,
            quantidadeEstoque: parseInt(document.getElementById('quantidadeEstoque').value),
            custoUnitario: parseFloat(document.getElementById('custoUnitario').value),
            fornecedorId: parseInt(document.getElementById('fornecedorId').value)
        };
        const res = await fetch('/api/pecas', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(payload) });
        if (res.ok) { alert('Peça salva!'); e.target.reset(); await carregarTabela(); } else alert('Erro: ' + await res.text());
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
    const res = await fetch('/api/pecas');
    if (res.ok) {
        const dados = await res.json();
        document.getElementById('tabelaDados').innerHTML = dados.map(x => `<tr><td>${x.id ?? x.Id}</td><td>${x.codigoSku ?? x.CodigoSku}</td><td>${x.nome ?? x.Nome}</td><td>${x.quantidadeEstoque ?? x.QuantidadeEstoque}</td><td>${x.custoUnitario ?? x.CustoUnitario}</td><td>${x.fornecedorId ?? x.FornecedorId}</td></tr>`).join('');
    }
}
