import { Component } from '@angular/core';

@Component({
  selector: 'app-teste',
  templateUrl: './teste.component.html',
})

export class TesteComponent {

  title: string = "Instruções de Serviço"
  dataResponse: Array<InstrucaoServico> =[];

  isExpanded:boolean = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  onSubmit() {
    console.log("Console vindo do arquivo TS");
    console.log(document.querySelector("#myform"));

    this.dataResponse = [];

    let form = document.querySelector("#myform") as HTMLFormElement;

    let formData = new FormData(form);

    for (let [key, value] of formData) {
      console.log(`Chave->${key} : Valor-> ${value}`);
    }

    const data = {
      codigoFilial: formData.get("codigoFilial").toString()
    };

    const meta = {
      method: 'get',
      headers: {
        'Authorization': 'Basic ' + btoa('MASTER:303304'),
        'Content-Type': 'application/json'
      },
    }

    fetch("http://195.1.1.110:53010/api/armazem/is/aberta?" + new URLSearchParams(data), meta)
      .then((response: any) => response.json())
      .then((data) => {
        for (let d in data) {
          console.log(`\n->${data[d]};\n`);
          this.dataResponse.push(data[d]);
        }

      })
      .catch((error: any) => {
        console.log("Error");
      });
  }

  
}

interface InstrucaoServico {
  id: number,
  numero: string
};
