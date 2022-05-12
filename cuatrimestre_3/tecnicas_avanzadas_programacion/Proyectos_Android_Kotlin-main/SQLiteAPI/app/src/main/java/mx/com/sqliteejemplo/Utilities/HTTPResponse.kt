package mx.com.sqliteejemplo.Utilities

interface HttpResponse {

    fun httpResponseSuccess(response: String)

    fun httpErrorResponse(response: String)
}