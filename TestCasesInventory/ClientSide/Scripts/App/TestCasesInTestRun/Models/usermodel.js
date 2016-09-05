define([], function () {
    function userModel(id, email, displayName, teamId) {
        this.ID = id;
        this.Email = email;
        this.DisplayName = displayName;
        this.TeamID = teamId;
    }
    return userModel;
});