mergeInto(LibraryManager.library, {
    //刷新数据到IndexedDB
    SyncDB: function () {
        FS.syncfs(false, function (err) {
           if (err) console.log("syncfs error: " + err);
        });
    }
});
