import React, { useEffect } from 'react';

import './App.css';

import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';
import logoCadastro from './assets/logo.png';


function App() {

  const baseUrl = "https://localhost:7078/api/alunos";
  const [data, setData] = React.useState([]);
  const [cont, setCont] = React.useState(1);

  const pedidoGet = async () => {
    await axios.get(baseUrl)
      .then(response => {
        setData(response.data)
      })
      .catch(error => {
        console.log("Error: " + error);
      });
  }

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
                <th scope="row">{cont}</th>
                <td>{aluno.id}</td>
                <td>{aluno.nome}</td>
                <td>{aluno.email}</td>
                <td>{aluno.idade}</td>
                <td>
                  <button className="btn btn-primary">Editar</button>
                  <button className="btn btn-danger">Excluir</button>
                </td>
                {setCont(cont)} 
              </tr>
            ))}
          </tbody>
        </table>
      </div>

    </div>
  );
}

export default App;
