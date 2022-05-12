package mx.com.sqliteejemplo.SQLite

import android.provider.BaseColumns

class AlumnosContract {

    companion object {

        val VERSION = 1

        class Entrada: BaseColumns{

            companion object {
                val NOMBRE_TABLA = "alumnos"
                val COLUMNA_ID = "id"
                val COLUMNA_NOMBRE = "nombre"
            }
        }
    }
}