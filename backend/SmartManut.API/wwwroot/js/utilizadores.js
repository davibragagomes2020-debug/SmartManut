document.addEventListener('DOMContentLoaded', async () => {
    await carregarSelect('/api/perfis', 'perfilId', p => p.id ?? p.Id, p => p.nomeCargo ?? p.NomeCargo);
    await carregarTabela();
    document.getElementById('formDados').addEventListener('submit', async (e) => {
        e.preventDefault();
        const payload = {
            nome: document.getElementById('nome').value,
            email: document.getElementById('email').value,
            senhaHash: "123456",
            ativo: true,
            perfilId: parseInt(document.getElementById('perfilId').value)
        };
        const res = await fetch('/api/utilizadores', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(payload) });
        if (res.ok) { alert('Utilizador salvo!'); e.target.reset(); await carregarTabela(); } else alert('Erro: ' + await res.text());
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
    const res = await fetch('/api/utilizadores');
    if (res.ok) {
        const dados = await res.json();
        document.getElementById('tabelaDados').innerHTML = dados.map(x => `<tr><td>${x.id ?? x.Id}</td><td>${x.nome ?? x.Nome}</td><td>${x.email ?? x.Email}</td><td>${x.perfilId ?? x.PerfilId}</td></tr>`).join('');
    }
}
