package mx.com.appcheckins

class FoursquareAPIRequestVenues {

    var meta:Meta? = null
    var response:FoursquareResponseVenue? = null

}

class Meta {
    var code:Int = 0
    var errorDetail:String = ""
}

class FoursquareResponseVenue {
    var venues: ArrayList<Venue>? = null
}

class Venue {
    var id:String = ""
    var name:String = ""
    var location:Location? = null
    var categories:ArrayList<Category>? = null
    var verified:Boolean? = null
    var hereNow:HereNow? = null
}

class Location {
    var lat:Double = 0.0
    var lng:Double = 0.0
    var state:String = ""
    var country:String = ""
}

class Category {
    var id:String = ""
    var name:String = ""
    var icon:Icon? = null
}

class Icon {
    var prefix:String = ""
    var suffix:String = ""
}

class HereNow {
    var count = 0
}
