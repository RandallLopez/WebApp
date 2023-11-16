import React, { useEffect, useState } from 'react';
import TablaLugaresEvento from './Tabla';

const Prueba = () => {
    const [lugaresEvento, setLugaresEvento] = useState([]);

    const obtenerLugares = async () => {
        const response = await fetch("api/lugarEvento/obtener")
        if (response.ok) {
            const data = await response.json()
            setLugaresEvento(data)
            console.log(response)
        } else {
            console.log("error al obtener lugares")
        }
    }

    useEffect(() => {
        obtenerLugares()
    }, [])

    const agregarLugarEvento = async (nuevoLugarEvento) => {
        const response = await fetch(window.location.origin + "/api/lugarEvento/agregar", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(nuevoLugarEvento),
        });
        obtenerLugares()
    };

    const editarLugarEvento = async (index, lugarEventoEditado) => {
        const response = await fetch(window.location.origin + "/api/lugarEvento/editar/" + parseInt(index), {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(lugarEventoEditado),
        });
        obtenerLugares()
    };

    const eliminarLugarEvento = async (index) => {
        const response = await fetch(window.location.origin + "/api/lugarEvento/eliminar/" + parseInt(index), {
            method: 'DELETE'
        });
        obtenerLugares()
    };

    return (
        <div>
            <h1>Tabla de Lugares de Evento</h1>
            <TablaLugaresEvento
                lugaresEvento={lugaresEvento}
                agregarLugarEvento={agregarLugarEvento}
                editarLugarEvento={editarLugarEvento}
                eliminarLugarEvento={eliminarLugarEvento}
            />
        </div>
    );
};

export default Prueba;
