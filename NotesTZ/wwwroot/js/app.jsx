class Note extends React.Component {

    constructor(props) {
        super(props);
        this.state = { data: props.note };
        this.onClick = this.onClick.bind(this);
    }
    onClick(e) {
        this.props.onRemove(this.state.data);
    }
    render() {
        return <tr>
            <td>{this.state.data.Id}</td>
            <td>{this.state.data.CreationDate}</td>
            <td>{this.state.data.Heading}</td>
            <td className="noteText">{this.state.data.Content}</td>
            <td><button onClick={this.onClick}>Delete</button></td>
        </tr>;
    }
}

class NoteAddForm extends React.Component {

    constructor(props) {
        super(props);
        this.state = { heading: "", content: "" };

        this.onSubmit = this.onSubmit.bind(this);
        this.onHeadingChange = this.onHeadingChange.bind(this);
        this.onContentChange = this.onContentChange.bind(this);
    }
    onHeadingChange(e) {
        this.setState({ heading: e.target.value });
    }
    onContentChange(e) {
        this.setState({ content: e.target.value });
    }
    onSubmit(e) {
        e.preventDefault();
        var headingValue = this.state.heading;
        var contentValue = this.state.content;
        if (!headingValue || contentValue <= 0) {
            return;
        }
        this.props.onNoteSubmit({ heading: headingValue, content: contentValue });
        this.setState({ heading: "", content: "" });
    }
    render() {
        return (
            <form onSubmit={this.onSubmit}>
                
                <p>
                    <input type="text"
                        placeholder="Heading"
                        value={this.state.heading}
                        onChange={this.onHeadingChange} />
                </p>
                <p>
                    <textarea type="text"
                        id="area"
                        placeholder="Text"
                        value={this.state.content}
                        onChange={this.onContentChange} />
                </p>

                <input type="submit" value="Save" />
            </form>
        );
    }
}

class NoteUpdateForm extends React.Component {

    constructor(props) {
        super(props);
        this.state = { heading: "", content: "", id: "" };

        this.onGetById = this.onGetById.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
        this.onHeadingChange = this.onHeadingChange.bind(this);
        this.onContentChange = this.onContentChange.bind(this);
        this.onIdChange = this.onIdChange.bind(this);
    }
    onHeadingChange(e) {
        this.setState({ heading: e.target.value });
    }
    onContentChange(e) {
        this.setState({ content: e.target.value });
    }
    onIdChange(e) {
        this.setState({ id: e.target.value });
    }
    onSubmit(e) {
        e.preventDefault();
        var headingValue = this.state.heading;
        var contentValue = this.state.content;
        var idValue = this.state.id;
        if (!headingValue || contentValue <= 0) {
            return;
        }
        this.props.onNoteUpdateSubmit({ heading: headingValue, content: contentValue, id: idValue });
        this.setState({ heading: "", content: "", id:"" });
    }

    onGetById(e) {

        if (this.state.id == "")
            return;

        var url = "/api/notes/" + this.state.id;
        
        var xhr = new XMLHttpRequest();
        xhr.open("get", url, true);
        xhr.onload = function () {           
            var data = JSON.parse(xhr.responseText);
            this.setState({ heading: data.Heading, content: data.Content, id: data.Id});
        }.bind(this);
        xhr.send();

                //this.props.onRemove(this.state.data);
    }

    render() {
        return (
            <form onSubmit={this.onSubmit}>
                <p>
                    <input type="text"
                        disabled={this.state.disabled}
                        placeholder="Id"
                        value={this.state.id}
                        onChange={this.onIdChange} />
                </p>
                <p>
                    <input type="text"
                        placeholder="Heading"
                        value={this.state.heading}
                        onChange={this.onHeadingChange} />
                </p>
                <p>
                    <textarea  type="text"
                        placeholder="Text"
                        value={this.state.content}
                        onChange={this.onContentChange} />
                </p>               
                <button onClick={this.onGetById}>Get note</button>
                <input type="submit" value="Update" />
            </form>
        );
    }
}

class Notes extends React.Component {

    constructor(props) {
        super(props);
        this.state = { notes: [] };

        this.onAddNote = this.onAddNote.bind(this);
        this.onRemoveNote = this.onRemoveNote.bind(this);
    }

    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", this.props.apiUrl, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ notes: data });
        }.bind(this);
        xhr.send();
    }

   
    componentDidMount() {
        this.loadData();
    }

    onRemoveNote(note) {

        if (note) {
            var url = this.props.apiUrl + "/" + note.Id;

            var xhr = new XMLHttpRequest();
            xhr.open("delete", url, true);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.onload = function () {
                if (xhr.status === 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send();
            
        }
    }

    onAddNote(note) {
        if (note) {

            const data = new FormData();
            data.append("heading", note.heading);
            data.append("content", note.content);
            var xhr = new XMLHttpRequest();

            xhr.open("post", this.props.apiUrl, true);
            xhr.onload = function () {
                if (xhr.status === 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send(data);
            
        }
        
    }

    onUpdateNote(note) {
        if (note) {

            var url = "/api/notes/" + note.id;

            const data = new FormData();
            //data.append("id", note.id);
            data.append("heading", note.heading);
            data.append("content", note.content);
            var xhr = new XMLHttpRequest();

            xhr.open("put", url, true);
            xhr.onload = function () {
                if (xhr.status === 200) {
                    
                }
            }.bind(this);
            xhr.send(data);
           
        }
    }
    render() {

        var remove = this.onRemoveNote;
        return <div className="mainBox">
            <div className="formBox">
            <div className="innerBox">
                    <h3>Add a new note</h3>
                <NoteAddForm onNoteSubmit={this.onAddNote} />
                </div>
                <div className="titleBox">
                    <h1>API Notes</h1>
                </div>
            <div className="innerBox">
                    <h3>Update the note</h3>
                <NoteUpdateForm onNoteUpdateSubmit={this.onUpdateNote} />
                </div>
            </div>
            <h2>List of notes</h2>
            <div className="boxTable">
                <table>
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Date</th>
                            <th>Heading</th>
                            <th>Text</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                    {
                    this.state.notes.map(function (note) {
                            return <Note key={note.Id} note={note} onRemove={remove} />
                        })
                    }
                    </tbody>
                </table>
            </div>
        </div>;
    }

}

ReactDOM.render(
    <Notes apiUrl="/api/notes" />,
    document.getElementById("content")
);