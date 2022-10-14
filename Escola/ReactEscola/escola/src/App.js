import React, { useEffect } from 'react';

import './App.css';

import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';
import logoCadastro from './assets/logo.png';


function App() {

  const baseUrl = "https://localhost:7078/api/alunos";
  const [data, setData] = React.useState([]);

  const [alunoSelecionado, setAlunoSelecionado] = React.useState({
    id: '',
    nome: '',
    email: '',
    idade: ''
  })
  
  const handleChange = e => {
    const {name, value} = e.target;
    setAlunoSelecionado({
      ...alunoSelecionado, [name]:value
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

  let i=1;

  useEffect(()=>{
    pedidoGet();
  });

  return (
    <div className="App">
      <br />
      <h3 className="border border-danger">Cadastro de Alunos</h3>
      <header className='text-start border border-danger'>
        <div className='d-flex'>
          <div className='ms-4'>
            <img src={logoCadastro} alt='Cadastro' />
          </div>
          <div className='mt-auto'>
            <button className='btn btn-primary border border-danger'>Cadastrar</button>
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
                <th scope="row">{i}</th>
                <td>{aluno.id}</td>
                <td>{aluno.nome}</td>
                <td>{aluno.email}</td>
                <td>{aluno.idade}</td>
                <td>
                  <button className="btn btn-primary">Editar</button>
                  <button className="btn btn-danger">Excluir</button>
                </td>
                 { i++ }
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      
      <Modal>
        <ModalHeader>Adicionar Aluno</ModalHeader>
        <ModalBody>
         <div className='form-group'>
          <label for="name">Nome</label>
          <input className='form-control' type="text" id="name"/>
          
          <label for="email">Email</label>
          <input className='form-control' type="email" id="email"/>
          
          <label for="idade">Idade</label>
          <input className='form-control' type="text" id="idade"/>
         </div>
        </ModalBody>
        <ModalFooter>
          <button className='btn btn-primary'>Incluir</button>
          <button className='btn btn-danger'>Cancelar</button>
        </ModalFooter>
      </Modal>

    </div>
  );
}

export default App;
