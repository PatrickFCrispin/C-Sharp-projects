class App {
    constructor() {
        this.inputCep = window.document.querySelector('#inputCep')
        this.btnPesquisar = window.document.querySelector('#btnPesquisar')
        this.spanMsgErro = window.document.querySelector('#spanMsgErro')
        this.txtAreaCep = window.document.querySelector('#txtAreaCep')

        this.inputCep.focus()
        this.RegisterHandlers()
    }

    RegisterHandlers() {
        inputCep.onkeydown = this.Limpar
        inputCep.onchange = this.Limpar
        btnPesquisar.onclick = this.GetCepAndUpdateView
    }

    Limpar() {
        spanMsgErro.innerHTML = ''
        txtAreaCep.innerHTML = ''
    }

    GetCepAndUpdateView() {
        if (inputCep.value.length < 8) {
            spanMsgErro.innerHTML = 'CEP inválido!'
            inputCep.focus()
            return
        }

        let req = new XMLHttpRequest()
        req.open('GET', `https://viacep.com.br/ws/${inputCep.value}/json/`)
        req.send(null)
        req.onreadystatechange = () => {
            if (req.readyState === 4) {   // api.readyState === XMLHttpRequest.DONE
                if (req.status === 200) {
                    let cepContent = JSON.parse(req.responseText)
                    if (cepContent.cep == null) {
                        spanMsgErro.innerHTML = 'CEP não encontrado.'
                        inputCep.focus()
                        return
                    }
                    txtAreaCep.innerHTML = JSON.stringify(cepContent, undefined, 4)
                    inputCep.value = ''
                    inputCep.focus()
                } else {
                    spanMsgErro.innerHTML = 'CEP inválido!'
                    inputCep.focus()
                }
            }   
        }
    }
}
new App()