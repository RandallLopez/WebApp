import React, { useState } from 'react';
import './Tabla.css';

const TablaLugaresEvento = ({ lugaresEvento, agregarLugarEvento, editarLugarEvento, eliminarLugarEvento }) => {
    const [edicionActiva, setEdicionActiva] = useState(false);
    const [nuevoLugarEvento, setNuevoLugarEvento] = useState({
        nombre: '',
        ciudad: '',
        pais: '',
        capacidad: '',
        deshabilitado: false
    });
    const [lugarEditado, setLugarEditado] = useState({
        nombre: '',
        ciudad: '',
        pais: '',
        capacidad: '',
        deshabilitado: false
    });
    const [edicionIndex, setEdicionIndex] = useState(-1);

    const handleEditar = (index, nombre, ciudad, pais, capacidad, deshabilitado) => {
        handleActualizarEditado(nombre, ciudad, pais, capacidad, deshabilitado);
        setEdicionIndex(index);
    };

    const handleActualizarEditado = (nuevoNombre, nuevoCiudad, nuevoPais, nuevoCapacidad, nuevoDeshabilitado) => {
        setLugarEditado({
            nombre: nuevoNombre,
            ciudad: nuevoCiudad,
            pais: nuevoPais,
            capacidad: nuevoCapacidad,
            deshabilitado: nuevoDeshabilitado
        });
    };

    const handleCancelarEdicion = () => {
        handleActualizarEditado('', '', '', 0, false);
        setEdicionIndex(-1);
    };


    const handleGuardar = (index) => {
        editarLugarEvento(index, lugarEditado);
        setEdicionIndex(-1);
    };


    const handleEliminar = (index) => {
        eliminarLugarEvento(index);
    };

    const handleAgregar = () => {
        agregarLugarEvento(nuevoLugarEvento);
        setNuevoLugarEvento({
            nombre: '',
            ciudad: '',
            pais: '',
            capacidad: '',
            deshabilitado: false
        });
    };

    const handleChange = (e) => {
        const { name, value } = e.target;

        // Si el campo es "capacidad", asegurarse de que sea un número positivo
        if (name === "capacidad") {
            const parsedValue = parseInt(value, 10);
            const newValue = isNaN(parsedValue) ? "" : Math.max(0, parsedValue);

            setNuevoLugarEvento((prevLugarEvento) => ({
                ...prevLugarEvento,
                [name]: newValue
            }));
        } else {
            setNuevoLugarEvento((prevLugarEvento) => ({
                ...prevLugarEvento,
                [name]: value
            }));
        }
    };

    const handleChangeEditado = (e) => {
        const { name, value } = e.target;

        // Si el campo es "capacidad", asegurarse de que sea un número positivo
        if (name === "capacidad") {
            const parsedValue = parseInt(value, 10);
            const newValue = isNaN(parsedValue) ? "" : Math.max(0, parsedValue);

            setLugarEditado((prevLugarEvento) => ({
                ...prevLugarEvento,
                [name]: newValue
            }));
        } else {
            setLugarEditado((prevLugarEvento) => ({
                ...prevLugarEvento,
                [name]: value
            }));
        }
    };
;

    return (
        <table>
            <thead>
                <tr>
                    <th>Nombre</th>
                    <th>Ciudad</th>
                    <th>Pais</th>
                    <th>Capacidad</th>
                    <th>Deshabilitado</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                {lugaresEvento.map((lugarEvento, index) => (
                    <tr key={index}>
                        <td>
                            {edicionIndex === index ? (
                                <input
                                    type="text"
                                    name="nombre"
                                    value={lugarEditado.nombre}
                                    onChange={handleChangeEditado}
                                />
                            ) : (
                                lugarEvento.nombre
                            )}
                        </td>
                        <td>
                            {edicionIndex === index ? (
                                <input
                                    type="text"
                                    name="ciudad"
                                    value={lugarEditado.ciudad}
                                    onChange={handleChangeEditado}
                                />
                            ) : (
                                lugarEvento.ciudad
                            )}
                        </td>
                        <td>
                            {edicionIndex === index ? (
                                <input
                                    type="text"
                                    name="pais"
                                    value={lugarEditado.pais}
                                    onChange={handleChangeEditado}
                                />
                            ) : (
                                lugarEvento.pais
                            )}
                        </td>
                        <td>
                            {edicionIndex === index ? (
                                <input
                                    type="number"
                                    name="capacidad"
                                    value={lugarEditado.capacidad}
                                    onChange={handleChangeEditado}
                                />
                            ) : (
                                lugarEvento.capacidad
                            )}
                        </td>
                        <td>
                            {edicionIndex === index ? (
                                <input type="checkbox" name="deshabilitado" checked={lugarEditado.deshabilitado}
                                    onChange={() =>
                                        setLugarEditado((prevLugarEditado) => ({
                                            ...prevLugarEditado,
                                            deshabilitado: !prevLugarEditado.deshabilitado
                                        }))}
                                />
                            ) : (
                                lugarEvento.deshabilitado ? 'Si' : 'No'
                            )}
                        </td>
                        <td>
                            {edicionIndex === index ? (
                                <>
                                    <button onClick={() => handleGuardar(lugarEvento.id)}>Guardar</button>
                                    <button onClick={handleCancelarEdicion}>Cancelar</button>
                                </>
                            ) : (
                                <>
                                        <button onClick={() => handleEditar(index,
                                            lugarEvento.nombre, lugarEvento.ciudad, lugarEvento.pais,
                                            lugarEvento.capacidad, lugarEvento.deshabilitado)}>Editar</button>
                                    <button onClick={() => handleEliminar(lugarEvento.id)}>Eliminar</button>
                                </>
                            )}
                        </td>
                    </tr>
                ))}
            </tbody>
            <tfoot>
                <tr>
                    <td>
                        <input type="text" name="nombre" placeholder="Nombre" value={nuevoLugarEvento.nombre}
                            onChange={handleChange}
                        />
                    </td>
                    <td>
                        <input type="text" name="ciudad" placeholder="Ciudad" value={nuevoLugarEvento.ciudad}
                            onChange={handleChange}
                        />
                    </td>
                    <td>
                        <input type="text" name="pais" placeholder="Pais" value={nuevoLugarEvento.pais}
                            onChange={handleChange}
                        />
                    </td>
                    <td>
                        <input type="number" name="capacidad" placeholder="Capacidad" value={nuevoLugarEvento.capacidad}
                            onChange={handleChange}
                        />
                    </td>
                    <td>
                        <input type="checkbox" name="deshabilitado" checked={nuevoLugarEvento.deshabilitado}
                            onChange={() =>
                                setNuevoLugarEvento((prevLugarEvento) => ({
                                    ...prevLugarEvento,
                                    deshabilitado: !prevLugarEvento.deshabilitado
                                }))}
                        />
                    </td>
                    <td>
                        <button onClick={handleAgregar}>Agregar</button>
                    </td>
                </tr>
            </tfoot>
        </table>
    );
};

export default TablaLugaresEvento;
