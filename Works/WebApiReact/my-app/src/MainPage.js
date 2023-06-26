import React, { Component } from 'react';
import { variables } from './Variables.js';
import { Login } from "./components/Login";
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import axios from "axios";
import { FormErrors } from './FormErrors';

export class MainPage extends Component {
    constructor(props) {
        super(props);

        this.state = {
            signs: [],
            sign: [],
            signId: 0,
            signName: "",
            street: "",
            city: "",
            coordinates: "",
            comment: "",
            incident: "",
            ice: "",
            email: "string",
            password: "string",
            log: false,
            formErrors: { email: '', password: '' },
            emailValid: false,
            passwordValid: false,
            formValid: false,
            photoFileName: "anonymous.png",
            PhotoPath: variables.PHOTO_URL,
            signsIdFilter: "",
            signsNameFilter: "",
            signsCityFilter: "",
            signsStreetFilter: "",
            signsWithoutFilter: [],
        }
    }

    refreshList() {
        fetch(variables.API_URL + 'Signs', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            }
        })
            .then(response => response.json())
            .then(data => {
                this.setState({ signs: data, signsWithoutFilter: data });
            });
    }

    componentDidMount() {
        this.refreshList();
    }

    changeSignName = (e) => {
        this.setState({ signName: e.target.value });
    }
    changeStreet = (e) => {
        this.setState({ street: e.target.value });
    }
    changeCity = (e) => {
        this.setState({ city: e.target.value });
    }
    changeCoordinates = (e) => {
        this.setState({ coordinates: e.target.value });
    }
    changeComment = (e) => {
        this.setState({ comment: e.target.value });
    }
    changeIncident = (e) => {
        this.setState({ incident: e.target.value });
        const name = e.target.name;
        const value = e.target.value;
        this.setState({ [name]: value },
            () => { this.validateField(name, value) });
    }
    changeIce = (e) => {
        this.setState({ ice: e.target.value });
    }
    changeLogin = (e) => {
        this.setState({ email: e.target.value });
        const name = e.target.name;
        const value = e.target.value;
        this.setState({ [name]: value },
            () => { this.validateField(name, value) });
    }
    changePassword = (e) => {
        this.setState({ password: e.target.value });
        const name = e.target.name;
        const value = e.target.value;
        this.setState({ [name]: value },
            () => { this.validateField(name, value) });
    }

    FilterFn() {
        var signsIdFilter = this.state.signsIdFilter;
        var signsNameFilter = this.state.signsNameFilter;
        var signsCityFilter = this.state.signsCityFilter;
        var signsStreetFilter = this.state.signsStreetFilter;

        var filteredData = this.state.signsWithoutFilter.filter(
            function (el) {
                return el.signId.toString().toLowerCase().includes(
                    signsIdFilter.toString().trim().toLowerCase()
                ) &&
                    el.signName.toString().toLowerCase().includes(
                        signsNameFilter.toString().trim().toLowerCase()
                    ) 
            }
        );

        this.setState({ signs: filteredData });

    }

    FilterFn2() {
        var signsIdFilter = this.state.signsIdFilter;
        var signsNameFilter = this.state.signsNameFilter;
        var signsCityFilter = this.state.signsCityFilter;
        var signsStreetFilter = this.state.signsStreetFilter;

        var filteredData = this.state.signsWithoutFilter.filter(
            function (el) {
                return el.city.toString().toLowerCase().includes(
                        signsCityFilter.toString().trim().toLowerCase()
                    ) 
            }
        );

        this.setState({ signs: filteredData });

    }

    changeSignIdFilter = (e)=>{
        this.state.signsIdFilter=e.target.value;
        this.FilterFn();
    }
    changeSignNameFilter = (e)=>{
        this.state.signsNameFilter=e.target.value;
        this.FilterFn();
    }
    changeCityFilter = (e)=>{
        this.state.signsCityFilter=e.target.value;
        this.FilterFn2();
    }
    changeStreetFilter = (e)=>{
        this.state.signsStreetFilter=e.target.value;
        this.FilterFn();
    }

    sortResult(prop, asc) {
        var sortedData = this.state.signsWithoutFilter.sort(function (a, b) {
            if (asc) {
                return (a[prop] > b[prop]) ? 1 : ((a[prop] < b[prop]) ? -1 : 0);
            }
            else {
                return (b[prop] > a[prop]) ? 1 : ((b[prop] < a[prop]) ? -1 : 0);
            }
        });

        this.setState({ signs: sortedData });
    }

    getClick(sig) {
        fetch(variables.API_URL + 'Signs/' + sig.signId, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            }
        })
            .then(res => res.json())
            .then((result) => {
                this.setState({ sign: result })
            }, (error) => {
                alert('Failed');
            })
        this.setState({
            modalTitle: "View Sign",
            signId: sig.signId,
            signName: sig.signName,
            street: sig.street,
            city: sig.city,
            coordinates: sig.coordinates,
            comment: sig.comment,
            incident: sig.incident,
            ice: sig.ice,
            photoFileName: sig.photoFileName
        });
    }

    createClick() {
        fetch(variables.API_URL + 'Signs', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('token')

            },
            body: JSON.stringify({
                signName: this.state.signName,
                street: this.state.street,
                city: this.state.city,
                coordinates: this.state.coordinates,
                comment: this.state.comment,
                incident: this.state.incident,
                ice: this.state.ice,
                photoFileName: this.state.photoFileName
            })
        })
            .then(res => res.json())
            .then((result) => {
                alert(result);
                this.refreshList();
            }, (error) => {
                alert('Failed');
            })
    }

    deleteClick(id) {
        if (window.confirm('Are you sure?')) {
            fetch(variables.API_URL + 'Signs/' + id, {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem('token')
                }
            })
                .then(res => res.json())
                .then((result) => {
                    alert(result);
                    this.refreshList();
                }, (error) => {
                    alert('Failed');
                })
        }
    }

    editClick(sig) {
        this.setState({
            modalTitle: "Edit Sign",
            signId: sig.signId,
            signName: sig.signName,
            street: sig.street,
            city: sig.city,
            coordinates: sig.coordinates,
            comment: sig.comment,
            incident: sig.incident,
            ice: sig.ice,
            photoFileName: sig.photoFileName
        });
    }

    loginclick() {
        this.setState({
            modalTitle: "Login",
        });
    }

    updateClick() {
        fetch(variables.API_URL + 'Signs', {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            },
            body: JSON.stringify({
                signId: this.state.signId,
                signName: this.state.signName,
                street: this.state.street,
                city: this.state.city,
                coordinates: this.state.coordinates,
                comment: this.state.comment,
                incident: this.state.incident,
                ice: this.state.ice,
                photoFileName: this.state.photoFileName
            })
        })
            .then(res => res.json())
            .then((result) => {
                alert(result);
                this.refreshList();
            }, (error) => {
                alert('Failed');
            })
    }

    imageUpload = (e) => {
        e.preventDefault();

        const formData = new FormData();
        formData.append("file", e.target.files[0], e.target.files[0].name);

        fetch(variables.API_URL + 'Signs/savefile', {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            },
            body: formData
        })
            .then(res => res.json())
            .then(data => {
                this.setState({ photoFileName: data });
            })
    }

    submit = (e) => {
        //e.preventDefault();

        fetch(variables.API_URL + 'login', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                email: this.state.email,
                password: this.state.password
            })
        })
            .then(res => res.json())
            .then((result) => {
                    localStorage.setItem('token', result);
                    this.setState({
                        log: true,
                    })
            }, (error) => {
                alert('Failed');
                this.setState({
                    log: false,
                })
            })
    }

    logout = () => {
        fetch(variables.API_URL + 'logout', {
            method: 'POST',
        })
            .then(res => res.json())
            .then((result) => {
                alert(result);
            }, (error) => {
                alert('Failed');
            })
        localStorage.setItem('token', "");
        this.setState({
            log: false,
        })
    }

    validateField(fieldName, value) {
        let fieldValidationErrors = this.state.formErrors;
        let emailValid = this.state.emailValid;
        let passwordValid = this.state.passwordValid;
        switch (fieldName) {
            case 'email':
                emailValid = value.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i);
                fieldValidationErrors.email = emailValid ? '' : ' is invalid';
                break;
            case 'password':
                passwordValid = value.length >= 6;
                fieldValidationErrors.password = passwordValid ? '' : ' is too short';
                break;
            default:
                break;
        }
        this.setState({
            formErrors: fieldValidationErrors,
            emailValid: emailValid,
            passwordValid: passwordValid
        }, this.validateForm);
    }
    validateForm() {
        this.setState({
            formValid: this.state.emailValid &&
                this.state.passwordValid
        });
    }
    /*submit = async e => {
        e.preventDefault();

        const {data} = await axios.post('login', {
            email: this.state.email, password: this.state.password
        });

        axios.defaults.headers.common['Authorization'] = `Bearer ${data['token']}`;
    }*/

    render() {
        const {
            signs,
            sign,
            signId,
            signName,
            street,
            city,
            coordinates,
            comment,
            incident,
            ice,
            modalTitle,
            PhotoPath,
            photoFileName,
            email,
            password,
            log,
        } = this.state;
        return (
            <div className="container">
                {/*<BrowserRouter>*/}
                {/*<div className="row"> 
                <Routes>

            <Route path="/login" element={<Login/>}/>

        </Routes>
                    </div>   */}
                <div className="row justify-content-end">
                    <button type="button"
                        className="btn btn-primary mr-1 col-2"
                        data-bs-toggle="modal"
                        data-bs-target="#loginModal"
                        onClick={() => this.loginClick()}>
                        Войти
                    </button>
                    <button type="button"
                        className="btn btn-secondary mr-1 col-2"
                        onClick={() => this.logout()}>
                        Выйти
                    </button>
                </div>

                {/*localStorage.getItem('token') !== "" ?*/}
                <div style={{ display: log ? "block" : "none" }}>
                    <div className="row" >
                        <div className="col-4">
                            <div className="input-group mb-3">
                                <span className="input-group-text">Название</span>
                                <input type="text" className="form-control"
                                    value={signName}
                                    onChange={this.changeSignName} />
                            </div>
                            <div className="input-group mb-3">
                                <span className="input-group-text">Улица</span>
                                <input type="text" className="form-control"
                                    value={street}
                                    onChange={this.changeStreet} />
                            </div>
                            <div className="input-group mb-3">
                                <span className="input-group-text">Город</span>
                                <input type="text" className="form-control"
                                    value={city}
                                    onChange={this.changeCity} />
                            </div>
                            <div className="input-group mb-3">
                                <span className="input-group-text">Координаты</span>
                                <input type="text" className="form-control"
                                    value={coordinates}
                                    onChange={this.changeCoordinates} />
                            </div>
                            <div className="input-group mb-3">
                                <span className="input-group-text">Комментарий</span>
                                <input type="text" className="form-control"
                                    value={comment}
                                    onChange={this.changeComment} />
                            </div>
                            <div className="input-group mb-3">
                                <span className="input-group-text">ДТП</span>
                                <input type="text" className="form-control"
                                    value={incident}
                                    onChange={this.changeIncident} />
                            </div>
                            <div className="input-group mb-3">
                                <span className="input-group-text">Гололед</span>
                                <input type="text" className="form-control"
                                    value={ice}
                                    onChange={this.changeIce} />
                            </div>
                        </div>
                        <div className="p-2 w-50 bd-highlight">
                            <img width="250px" height="250px"
                                src={PhotoPath + photoFileName} />
                            <input className="m-2" type="file" onChange={this.imageUpload} />
                        </div>
                        <div className="panel panel-default">
                            <FormErrors formErrors={this.state.formErrors} />
                        </div>
                    </div>
                    <div className="row">
                        <button type="button"
                            className="btn col-4 btn-primary float-start"
                            onClick={() => this.createClick()}
                        >Создать</button>
                    </div>
                </div>
                {/*} : null*/}

                <div className="row">
                    {/*
                        <ul className="list-group">
                            {signs.map((sig) =>
                                <li className="list-group-item" key={sig.signId} onClick={() => this.getClick(sig)}>
                                    {sig.signName}
                                    {/*<button type="button"
                                        className="btn btn-primary float-start"
                                        onClick={() => this.deleteClick(sig.signId)}
                            >Удалить</button>
                                </li>)}
                        </ul>
                        */}
                    <table className="table table-striped">
                        <thead>
                            <tr>
                                <th>
                                    <div className="d-flex flex-row">
                                        <input className="form-control m-2"
                                            onChange={this.changeSignIdFilter}
                                            placeholder="Filter" />

                                        <button type="button" className="btn btn-light"
                                            onClick={() => this.sortResult('signId', true)}>
                                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                                                <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z" />
                                            </svg>
                                        </button>

                                        <button type="button" className="btn btn-light"
                                            onClick={() => this.sortResult('signId', false)}>
                                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-up-square-fill" viewBox="0 0 16 16">
                                                <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z" />
                                            </svg>
                                        </button>

                                    </div>
                                    SignId
                                </th>
                                <th>
                                <div className="d-flex flex-row">
                                        <input className="form-control m-2"
                                            onChange={this.changeSignNameFilter}
                                            placeholder="Filter" />

                                        <button type="button" className="btn btn-light"
                                            onClick={() => this.sortResult('signName', true)}>
                                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                                                <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z" />
                                            </svg>
                                        </button>

                                        <button type="button" className="btn btn-light"
                                            onClick={() => this.sortResult('signName', false)}>
                                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-up-square-fill" viewBox="0 0 16 16">
                                                <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z" />
                                            </svg>
                                        </button>

                                    </div>
                                    Название
                                </th>
                                <th>
                                <div className="d-flex flex-row">
                                        <input className="form-control m-2"
                                            onChange={this.changeStreetFilter}
                                            placeholder="Filter" />

                                        <button type="button" className="btn btn-light"
                                            onClick={() => this.sortResult('street', true)}>
                                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                                                <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z" />
                                            </svg>
                                        </button>

                                        <button type="button" className="btn btn-light"
                                            onClick={() => this.sortResult('street', false)}>
                                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-up-square-fill" viewBox="0 0 16 16">
                                                <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z" />
                                            </svg>
                                        </button>

                                    </div>
                                    Улица
                                </th>
                                <th>
                                <div className="d-flex flex-row">
                                        <input className="form-control m-2"
                                            onChange={this.changeCityFilter}
                                            placeholder="Filter" />

                                        <button type="button" className="btn btn-light"
                                            onClick={() => this.sortResult('city', true)}>
                                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                                                <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z" />
                                            </svg>
                                        </button>

                                        <button type="button" className="btn btn-light"
                                            onClick={() => this.sortResult('city', false)}>
                                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-up-square-fill" viewBox="0 0 16 16">
                                                <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z" />
                                            </svg>
                                        </button>

                                    </div>
                                    Город
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            {signs.map(sig =>
                                <tr key={sig.signId} data-bs-toggle="modal"
                                    data-bs-target="#viewModal" onClick={() => this.getClick(sig)}>
                                    <td>{sig.signId}</td> {/*  заменить sig на sign? */}
                                    <td>{sig.signName}</td>
                                    <td>{sig.street}</td>
                                    <td>{sig.city}</td>
                                    <td>{sig.coordinates}</td>
                                    <td>{sig.comment}</td>
                                    <td>{sig.incident}</td>
                                    <td>{sig.ice}</td>
                                    <td>{sig.photoFileName}</td>
                                    <td>
                                        <button type="button"
                                            className="btn btn-light mr-1"
                                            data-bs-toggle="modal"
                                            data-bs-target="#exampleModal"
                                            onClick={() => this.editClick(sig)}
                                            style={{ display: log ? "inline-block" : "none" }}>
                                            Edit
                                        </button>

                                        <button type="button"
                                            className="btn btn-light mr-1"
                                            onClick={() => this.deleteClick(sig.signId)}
                                            style={{ display: log ? "inline-block" : "none" }}>
                                            Delete
                                        </button>

                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
                <div className="modal fade" id="exampleModal" tabIndex="-1" aria-hidden="true">
                    <div className="modal-dialog modal-lg modal-dialog-centered">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title">{modalTitle}</h5>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"
                                ></button>
                            </div>

                            <div className="modal-body">
                                <div className="d-flex flex-row bd-highlight mb-3">

                                    <div className="p-2 w-50 bd-highlight">

                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Название</span>
                                            <input type="text" className="form-control"
                                                value={signName}
                                                onChange={this.changeSignName} />
                                        </div>

                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Улица</span>
                                            <input type="text" className="form-control"
                                                value={street}
                                                onChange={this.changeStreet} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Город</span>
                                            <input type="text" className="form-control"
                                                value={city}
                                                onChange={this.changeCity} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Координаты</span>
                                            <input type="text" className="form-control"
                                                value={coordinates}
                                                onChange={this.changeCoordinates} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Комментарий</span>
                                            <input type="text" className="form-control"
                                                value={comment}
                                                onChange={this.changeComment} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">ДТП</span>
                                            <input type="text" className="form-control"
                                                value={incident}
                                                onChange={this.changeIncident} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Гололед</span>
                                            <input type="text" className="form-control"
                                                value={ice}
                                                onChange={this.changeIce} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Файл</span>
                                            <input type="text" className="form-control"
                                                value={photoFileName}
                                            />
                                        </div>
                                        {/*
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Department</span>
                                            <select className="form-select"
                                                onChange={this.changeSign}
                                                value={sign}>
                                                {signs.map(sig => <option key={sig.signId}>
                                                    {sig.signName}
                                                </option>)}
                                            </select>
                                        </div>
                                                */}
                                        {/*}
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">DOJ</span>
                                            <input type="date" className="form-control"
                                                value={DateOfJoining}
                                                onChange={this.changeDateOfJoining} />
                                        </div>
                                                */}

                                    </div>
                                    <div className="p-2 w-50 bd-highlight">
                                        <img width="250px" height="250px"
                                            src={PhotoPath + photoFileName} />
                                        <input className="m-2" type="file" onChange={this.imageUpload} />
                                    </div>
                                </div>

                                {signId === 0 ?
                                    <button type="button"
                                        className="btn btn-primary float-start"
                                        onClick={() => this.createClick()}
                                    >Create</button>
                                    : null}

                                {signId !== 0 ?
                                    <button type="button"
                                        className="btn btn-primary float-start"
                                        onClick={() => this.updateClick()}
                                    >Update</button>
                                    : null}
                            </div>

                        </div>
                    </div>
                </div>

                <div className="modal fade" id="viewModal" tabIndex="-1" aria-hidden="true">
                    <div className="modal-dialog modal-lg modal-dialog-centered">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title">{modalTitle}</h5>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"
                                ></button>
                            </div>

                            <div className="modal-body">
                                <div className="d-flex flex-row bd-highlight mb-3">

                                    <div className="p-2 w-50 bd-highlight">

                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Название</span>
                                            <input type="text" readonly className="form-control"
                                                value={signName}
                                            />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Улица</span>
                                            <input type="text" readonly className="form-control"
                                                value={street}
                                                onChange={this.street} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Город</span>
                                            <input type="text" readonly className="form-control"
                                                value={city}
                                                onChange={this.city} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Координаты</span>
                                            <input type="text" readonly className="form-control"
                                                value={coordinates}
                                                onChange={this.coordinates} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Комментарий</span>
                                            <input type="text" readonly className="form-control"
                                                value={comment}
                                                onChange={this.comment} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">ДТП</span>
                                            <input type="text" readonly className="form-control"
                                                value={incident}
                                                onChange={this.incident} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Гололед</span>
                                            <input type="text" readonly className="form-control"
                                                value={ice}
                                                onChange={this.ice} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Файл</span>
                                            <input type="text" readonly className="form-control"
                                                value={photoFileName}
                                            />
                                        </div>

                                    </div>
                                    <div className="p-2 w-50 bd-highlight">
                                        <img width="250px" height="250px"
                                            src={PhotoPath + photoFileName} />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div className="modal fade" id="loginModal" tabIndex="-1" aria-hidden="true">
                    <div className="modal-dialog modal-lg modal-dialog-centered">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title">{modalTitle}</h5>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"
                                ></button>
                            </div>

                            <div className="modal-body">
                                <div className="panel panel-default">
                                    <FormErrors formErrors={this.state.formErrors} />
                                </div>
                                <div className="d-flex flex-row bd-highlight mb-3">
                                    <div className="p-2 w-50 bd-highlight">

                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Почта</span>
                                            <input type="text" name="email" className="form-control"
                                                value={email}
                                                onChange={this.changeLogin} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Пароль</span>
                                            <input type="password" name="password" className="form-control"
                                                value={password}
                                                onChange={this.changePassword}
                                            />
                                        </div>
                                    </div>
                                </div>


                                <button type="button"
                                    className="btn btn-primary float-start"
                                    disabled={!this.state.formValid}
                                    onClick={() => this.submit()}
                                >Войти</button>

                            </div>

                        </div>
                    </div>
                </div>
                {/* </BrowserRouter> */}
            </div>
        )
    }
}