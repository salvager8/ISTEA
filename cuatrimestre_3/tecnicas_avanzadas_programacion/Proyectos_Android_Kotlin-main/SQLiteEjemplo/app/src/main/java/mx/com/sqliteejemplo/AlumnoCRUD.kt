package mx.com.sqliteejemplo

import android.content.ContentValues
import android.content.Context
import android.database.Cursor
import android.database.sqlite.SQLiteDatabase

class AlumnoCRUD(context: Context) {

    private var helper:DataBaseHelper? = null

    init {
        helper = DataBaseHelper(context)
    }

    fun nuevoAlumno(item:Alumno) {
        //Abrir la DB en modo escritura
        val db:SQLiteDatabase = helper?.writableDatabase!!

        //Mapeo de las columnas con valores a insertar
        val values = ContentValues()
        values.put(AlumnosContract.Companion.Entrada.COLUMNA_ID, item.id)
        values.put(AlumnosContract.Companion.Entrada.COLUMNA_NOMBRE, item.nombre)

        //Insertar una nueva fila en la tabla
        val newRowId = db.insert(AlumnosContract.Companion.Entrada.NOMBRE_TABLA, null, values)

        //Cerrar la conexion
        db.close()
    }

    fun getAlumnos():ArrayList<Alumno> {

        val items:ArrayList<Alumno> = ArrayList()

        //Abrir DB en modo lectura
        val db:SQLiteDatabase = helper?.readableDatabase!!

        //Especificar columnas que quiero consultar
        val columnas = arrayOf(
            AlumnosContract.Companion.Entrada.COLUMNA_ID,
            AlumnosContract.Companion.Entrada.COLUMNA_NOMBRE
        )

        //Crear un cursos para recorrer la tabla
        val c:Cursor = db.query(
            AlumnosContract.Companion.Entrada.NOMBRE_TABLA,
            columnas,
            null,
            null,
            null,
            null,
            null
        )

        //Hacer el recorrido del cursor en la tabla
        while (c.moveToNext()) {
            items.add(Alumno(
                c.getString(c.getColumnIndexOrThrow(AlumnosContract.Companion.Entrada.COLUMNA_ID)),
                c.getString(c.getColumnIndexOrThrow(AlumnosContract.Companion.Entrada.COLUMNA_NOMBRE))
            )
            )
        }
        //Cerrar DB
        db.close()

        return items
    }

    fun getAlumno(id:String):Alumno {
        var item: Alumno? = null

        //Abrir DB en modo lectura
        val db: SQLiteDatabase = helper?.readableDatabase!!

        //Especificar columnas que quiero consultar
        val columnas = arrayOf(AlumnosContract.Companion.Entrada.COLUMNA_ID, AlumnosContract.Companion.Entrada.COLUMNA_NOMBRE)

        //Crear un cursos para recorrer la tabla
        val c:Cursor = db.query(
            AlumnosContract.Companion.Entrada.NOMBRE_TABLA,
            columnas,
            " id = ?",
            arrayOf(id),
            null,
            null,
            null
        )

        while (c.moveToNext()) {
            item = Alumno(c.getString(c.getColumnIndexOrThrow(AlumnosContract.Companion.Entrada.COLUMNA_ID)),
                c.getString(c.getColumnIndexOrThrow(AlumnosContract.Companion.Entrada.COLUMNA_NOMBRE)))
        }
        c.close()

        return item!!
    }

    fun updateAlumno(item:Alumno) {

        //Abrir DB en modo escritura
        val db: SQLiteDatabase = helper?.writableDatabase!!

        //Mapeo de las columnas con valores a insertar
        val values = ContentValues()
        values.put(AlumnosContract.Companion.Entrada.COLUMNA_ID, item.id)
        values.put(AlumnosContract.Companion.Entrada.COLUMNA_NOMBRE, item.nombre)

        //Actualizar una nueva fila en la tabla
        db.update(AlumnosContract.Companion.Entrada.NOMBRE_TABLA, values, "id = ?", arrayOf(item.id))

        db.close()
    }

    fun deleteAlumno(item:Alumno) {

        //Abrir DB en modo escritura
        val db: SQLiteDatabase = helper?.writableDatabase!!

        db.delete(AlumnosContract.Companion.Entrada.NOMBRE_TABLA, "id = ?", arrayOf(item.id))

        db.close()

    }

}