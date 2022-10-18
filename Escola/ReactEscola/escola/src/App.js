import React, { useEffect } from 'react';

import './App.css';

import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';
import logoCadastro from './assets/logo.png';


function App() {

  const baseUrl = "https://localhost:7078/api/alunos";
  const [data, setData] = React.useState([]);
  const [modalIncluir, setModalIncluir] = React.useState(false);
  const [modalEditar, setModalEditar] = React.useState(false);

  const [alunoSelecionado, setAlunoSelecionado] = React.useState({
    id: '',
    nome: '',
    email: '',
    idade: ''
  })

  const abrirFecharModal = () => {
    setModalIncluir(!modalIncluir);
  }

  const abrirFecharModalEditar = () => {
    setModalEditar(!modalEditar);
  }

  const selecionarAluno = (aluno, opcao) => {
    setAlunoSelecionado(aluno);
    (opcao === "Editar") &&
      abrirFecharModalEditar()
  }

  const handleChange = e => {
    const { name, value } = e.target;
    setAlunoSelecionado({
      ...alunoSelecionado,
      [name]: value
    });
    console.log(alunoSelecionado);
  }

  const pedidoGet = async () => {
    await axios.get(baseUrl)
      .then(response => {
        setData(response.data)
      })
      .catch(error => {
        console.log("Error: " + error);
      });
  };

  const pedidoPost = async () => {
    delete alunoSelecionado.id;
    console.log("Dados a serem enviados: " + Object.values(alunoSelecionado));
    alunoSelecionado.idade = parseInt(alunoSelecionado.idade);
    axios.post(baseUrl, alunoSelecionado)
      .then(response => {
        setData(data.concat(response.data));
        abrirFecharModal();
      })
      .catch(error => {
        console.log(error);
        alert("Ja existe um aluno com esse e-mail cadastrado");
      })
  }
  const pedidoPut = async () => {
    alunoSelecionado.idade = parseInt(alunoSelecionado.idade);

    await axios.put(baseUrl + "/" + alunoSelecionado.id, alunoSelecionado)
    .then(response => {
      let resposta = response.data;
      let dadosAuxiliar = data;
      dadosAuxiliar.map(aluno =>{
        if(aluno.id === alunoSelecionado.id){
          aluno.nome = resposta.nome;
          aluno.email = resposta.email;
          aluno.idade = resposta.idade;
        }
      });
      abrirFecharModalEditar();
    })
    .catch(error => {
      console.log("Erro: "+error);
    })
}

let i = 1;

useEffect(() => {
  pedidoGet();
}, [alunoSelecionado]);

return (
  <div className="App">
    <br />
    <h3 >Cadastro de Alunos</h3>
    <header className='text-start'>
      <div className='d-flex'>
        <div className='ms-4'>
          <img src={logoCadastro} alt='Cadastro' />
        </div>
        <div className='mt-auto'>
          <button className='btn btn-primary' onClick={() => { abrirFecharModal() }}>Cadastrar</button>
        </div>
      </div>
    </header>

    <div className="container mt-5">
      <table className="table table-striped">
        <thead className="bg-dark text-white border">
          <tr>
            <th scope="col">#</th>
            <th scope="col">Id</th>
            <th scope="col">Nome</th>
            <th scope="col">E-mail</th>
            <th scope="col">Idade</th>
            <th scope="col"></th>
          </tr>
        </thead>
        <tbody>
          {data.map(aluno => (
            <tr key={aluno.id}>
              <th scope="row">{i++}</th>
              <td>{aluno.id}</td>
              <td>{aluno.nome}</td>
              <td>{aluno.email}</td>
              <td>{aluno.idade}</td>
              <td>
                <button className="btn btn-primary me-3" onClick={() => { selecionarAluno(aluno, "Editar") }}>Editar</button>
                <button className="btn btn-danger" onClick={() => { selecionarAluno(aluno, "Excluir") }}>Excluir</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>

    <Modal isOpen={modalIncluir}>
      <ModalHeader>Adicionar Aluno</ModalHeader>
      <ModalBody>
        <div className='form-group'>
          <label htmlFor="name">Nome</label>
          <input className='form-control' type="text" id="name" name="nome" onChange={handleChange} /><br/>

          <label htmlFor="email">Email</label>
          <input className='form-control' type="email" id="email" name="email" onChange={handleChange} /><br/>

          <label htmlFor="idade">Idade</label>
          <input className='form-control' type="text" id="idade" name="idade" onChange={handleChange} /><br/>
        </div>
      </ModalBody>
      <ModalFooter>
        <button className='btn btn-primary' onClick={() => { pedidoPost() }}>Incluir</button>
        <button className='btn btn-danger' onClick={() => { abrirFecharModal() }}>Cancelar</button>
      </ModalFooter>
    </Modal>

    <Modal isOpen={modalEditar}>
      <ModalHeader>Editar Aluno</ModalHeader>
      <ModalBody>
        <div className='form-group'>
          <label>ID: </label>
          <input type="text" className='form-control' readOnly value={alunoSelecionado && alunoSelecionado.id} />
          <br />

          <label>Nome: </label><br />
          <input type="text" className='form-control' name='nome' value={alunoSelecionado && alunoSelecionado.nome} onChange={handleChange} />
          <br />

          <label>Email</label><br />
          <input type="email" className='form-control' name='email' value={alunoSelecionado && alunoSelecionado.email} onChange={handleChange} />
          <br />

          <label>Idade</label><br />
          <input type="text" className='form-control' name='idade' value={alunoSelecionado && alunoSelecionado.idade} onChange={handleChange} />
          <br />

        </div>
      </ModalBody>
      <ModalFooter>
        <button className='btn btn-primary' onClick={() => {pedidoPut()}}>Editar</button>
        <button className='btn btn-danger' onClick={() => { abrirFecharModalEditar() }}>Cancelar</button>
      </ModalFooter>
    </Modal>

  </div>
);
}

export default App;
