import os, sys
import json
from tkinter import *
from functools import partial
import threading
import time

try:
    from PIL import Image, ImageTk, ImageDraw, ImageFont
except ModuleNotFoundError:
    print("You are missing a necessary module - PIL.")

class Application():
    def __init__(self) -> None:
        self.file_targ = ""
        self.open_file = None
        self.working_str = ""
        self.bpm = 4
        self.beat_map_settings = {
            "approachTimeDrum": 1.0,
            "approachTimeGuitar": 2.0,
            "numNotesDrum": 3,
            "numNotesGuitar": 3,
            "excellentWindow": 0.1,
            "goodWindow": 0.2,
            "badWindow": 0.3,
            "noteTimesDrum": [],
            "noteLocationsDrum": [],
            "noteTimesGuitar": [],
            "noteLocationsGuitar": [],
            "noteTypesGuitar": [],
            "numberOfGuitarNotes" : 4,
            "numberOfDrums" : 4,
            "beatsPerSecond" : 0
        }

        self.available_levels = []
        self.left_mode = "none"
        self.right_mode = "guitar"
        self.recently_saved = True
        self.force_open_pending = False

    def save(self, file_name=None):
        pass

    def load():
        pass

class ChartEvent():
    def __init__(self) -> None:
        self.time = 0.0
        self.length: int = 1
        self.note: int = 0

class EditorCanvas(Canvas):
    def __init__(self, master, **kwargs):
        super().__init__(master, kwargs)

        self.info: str = self.master.info
        self.canvas_mode: str = self.info.right_mode
        self.event_grid = []

        self.scroller = Scrollbar(self)
        self.configure(yscrollcommand=self.scroller.set)
        self.scroller.configure(command=self.yview)

        self.scroller.pack(side=RIGHT, fill=Y)

        self.x_margin = 20
        self.y_margin = 30
        self.cell_size = 50
        self.configure(scrollregion=(0, 0, self.x_margin * 2 + self.cell_size * 8,
                                    self.y_margin + self.cell_size * 8))

        
        for _ in range(100):
            self.event_grid.append([None] * 8)

        self.draw_table()

    def draw_table(self):
        for y, row in enumerate(self.event_grid):
            for x, eve in enumerate(row):
                numNotes = 0
                if self.canvas_mode == "drum":
                    numNotes = self.info.beat_map_settings["numberOfDrumNotes"]
                elif self.canvas_mode == "guitar":
                    numNotes = self.info.beat_map_settings["numberOfGuitarNotes"]

                if x < numNotes:
                    self.create_rectangle(x * self.cell_size + self.x_margin,
                                        y * self.cell_size + self.y_margin,
                                        (x + 1) * self.cell_size + self.x_margin,
                                        (y + 1) * self.cell_size + self.y_margin, fill="#a6a6a6", outline="#000000")
                else:
                    self.create_rectangle(x * self.cell_size + self.x_margin,
                                        y * self.cell_size + self.y_margin,
                                        (x + 1) * self.cell_size + self.x_margin,
                                        (y + 1) * self.cell_size + self.y_margin, fill="#323232", outline="#f0f0ff")

                if eve: pass

    def events(self):
        pass

    def update(self):
        self.draw_table()
    

class Window(PanedWindow):
    def __init__(self, master=None) -> None:
        super().__init__(master, orient=VERTICAL)
        self.w_w = 800
        self.w_h = 700
        self.master.geometry("800x700+0+0")
        self.master.title("Charter")
        self.info = Application()

        self.up_pane = LabelFrame(self, bg="#0f0f0f", height=23, width=self.w_w)
        self.add(self.up_pane)

        self.working_area = PanedWindow(self, bg="#3f3f3f", height=self.w_h-23, width=self.w_w)
        self.add(self.working_area)
        self.options = LabelFrame(self, bg="#0f0f0f", height=self.w_h - 10, width=self.w_w // 4)
        self.chartview = EditorCanvas(self, bg="#eeeeee", height=self.w_h - 10, width=self.w_w // (4 / 3), confine=False)
        self.working_area.add(self.options)
        self.working_area.add(self.chartview)

        self.menu_buttons = {
            "file": [Button(self.up_pane, text="FILE", activeforeground="#eeeeee",
                 activebackground="#5f5f5f", height=1, width=5,
                 bg="#0f0f0f", fg="#ffffff", command=self.doFileMenu),
             (0, 0)],
            "edit": [Button(self.up_pane, text="EDIT", activeforeground="#eeeeee",
                activebackground="#5f5f5f", height=1, width=5,
                bg="#0f0f0f", fg="#ffffff", command=self.doEditMenu),
             (0, 1)],
            "help": [Button(self.up_pane, text="ABOUT", activeforeground="#eeeeee",
                activebackground="#5f5f5f", height=1, width=5,
                bg="#0f0f0f", fg="#ffffff", command=self.doAboutMenu),
             (0, 2)],
        }

        for b in self.menu_buttons.values():
            b[0].place(relheight=1.0, relwidth=0.05, relx=b[1][1] * 0.06 + 0.02, rely=b[1][0] * 0)
            
        self.emptyMenu()
        self.pack()

        #relevant variables
        self.file_name = StringVar(self.options)
        self.instrument_type = StringVar(self, value="guitar")
        self.windows = {
            "bad": DoubleVar(self, value=0.3),
            "good": DoubleVar(self, value=0.2),
            "excellent": DoubleVar(self, value=0.1)
        }
 
        self.isRunning = True
        self.parallel = threading.Thread(target=self.do, daemon=True)
        self.parallel.start()
        self.master.protocol("WM_DELETE_WINDOW", self.quit)
        
        
    def do(self):
        while self.isRunning:
            print("woomf")
            self.chartview.update()
            time.sleep(0.05)

    def quit(self):
        self.isRunning = False
        self.parallel.join(0.5)
        self.master.destroy()

    def emptyMenu(self):
        self.title = Text(self.options, bg="#0f0f0f", fg="#ffffff", relief=FLAT)
        self.title.insert(INSERT, "Select FILE to begin.")
        self.title.place(rely=0.05, relx=0.06, relwidth=0.88, relheight=0.05)

    def unemptyMenu(self):
        self.title.destroy()

    def doFileMenu(self):
        if self.info.left_mode == "file":
            self.leaveFileMenu()
            self.emptyMenu()
            self.info.left_mode = "none"
        else:
            if self.info.left_mode == "none":
                self.unemptyMenu()
            elif self.info.left_mode == "edit":
                self.leaveEditMenu()
            elif self.info.left_mode == "about":
                self.leaveAboutMenu()
            
            self.enterFileMenu()
            self.info.left_mode = "file"

    def enterFileMenu(self):
        self.file_entry_label = Label(self.options, bg="#0f0f0f", fg="#ffffff", text="File Name:")
        self.file_entry = Entry(self.options, fg="#afefef", bg="#3f3f3f", exportselection=0,
         textvariable=self.file_name, width=35)
        self.file_enterer = Button(self.options, bg="#0f0f0f", fg="#0f0f0f",
         height=self.file_entry.winfo_height(), command=self.enterFileName)

        self.file_entry_label.place(relx=0.01, rely=0.05)
        self.file_entry.place(relx=0.2, rely=0.05)
        self.file_enterer.place(relx=0.8, rely=0.05)
        self.working_area.sash_place(0, self.w_w // 2, 0)

        self.open_button = Button(self.options, text="OPEN", bg="#0f0f0f", fg="#3f3f3f", command=self.openFile)
        self.save_button = Button(self.options, text="SAVE", bg="#0f0f0f", fg="#3f3f3f", command=self.saveFile)
        self.saveas_button = Button(self.options, text="SAVE AS", bg="#0f0f0f", fg="#3f3f3f", command=partial(self.saveFile, False))

        self.open_button.place(relx=0.2, rely=0.3)
        self.save_button.place(relx=0.5, rely=0.3)
        self.saveas_button.place(relx=0.8, rely=0.3)
        
        self.message_content = ""
        self.message = Label(self.options, fg="#3f3f3f", bg="#0f0f0f", text=self.message_content)
        self.message.place(relx=0.2, rely=0.5)

    def leaveFileMenu(self):
        self.file_entry.destroy()
        self.file_entry_label.destroy()
        self.file_enterer.destroy()
        self.open_button.destroy()
        self.save_button.destroy()
        self.saveas_button.destroy()
        self.message.destroy()
        self.working_area.sash_place(0, self.w_w // 3, 0)
    
    def enterFileName(self):
        if self.file_name.get() != "":
            self.info.file_targ = f"Charts{os.sep}{self.file_name.get()}.txt"

    def saveFile(self, useWorkingFile=True):
        if self.info.file_targ != "":
            self.info.recently_saved = True
            if useWorkingFile:
                self.info.save()
                self.message_content = f"Saved work to {self.info.open_file}"
            else:
                self.info.save(self.info.file_targ)
                self.message_content = f"Saved work to {self.info.file_targ}"
            self.message.config(text=self.message_content)

    def openFile(self):
        if self.info.file_targ != "":
            if self.info.recently_saved:
                self.info.load()
                self.message_content = f"Opened {self.info.file_targ}."
                self.info.recently_saved = True
                self.info.open_file = self.info.file_targ
                self.info.file_targ = ""
            else:
                if self.info.force_open_pending:
                    self.info.force_open_pending = True
                    self.message_content = f"Opened {self.info.file_targ}. All progress was overwritten."
                    self.info.open_file = self.info.file_targ
                    self.info.file_targ = ""
                else:
                    self.info.force_open_pending = False
                    self.message_content = "You have not yet saved. Would you like to save?"

            self.message.config(text=self.message_content)

    def doEditMenu(self):
        if self.info.left_mode == "edit":
            self.leaveEditMenu()
            self.emptyMenu()
            self.info.left_mode = "none"
        else:
            if self.info.left_mode == "none":
                self.unemptyMenu()
            elif self.info.left_mode == "file":
                self.leaveFileMenu()
            elif self.info.left_mode == "about":
                self.leaveAboutMenu()
            
            self.enterEditMenu()
            self.info.left_mode = "edit"
    
    def leaveEditMenu(self):
        for snoot in self.snootsN_whoosles.values():
            snoot.destroy()

        for l in self.labelles.values():
            l.destroy()

    def enterEditMenu(self):
        self.snootsN_whoosles = {
            #"instr_toggle1" : Radiobutton(self.options, text="Guitar", bg="#0f0f0f", fg="#5f6363",
            #                        activebackground="#0f0f0f", activeforeground="#7f8383",
            #                        variable=self.instrument_type, value="guitar"),
            #"instr_toggle2" : Radiobutton(self.options, text="Drum", bg="#0f0f0f", fg="#5f6363",
            #                        activebackground="#0f0f0f", activeforeground="#7f8383",
            #                        variable=self.instrument_type, value="drum"),
            "bad_window" : Spinbox(self.options, from_=0.0, to=1.0, increment=0.01,
                                    bg="#0f0f0f", fg="#5f6363",
                                    activebackground="#0f0f0f"),
            "good_window" : Spinbox(self.options, from_=0.0, to=1.0, increment=0.01,
                                    bg="#0f0f0f", fg="#5f6363",
                                    activebackground="#0f0f0f"),
            "excellent_window" : Spinbox(self.options, from_=0.0, to=1.0, increment=0.01,
                                        bg="#0f0f0f", fg="#5f6363",
                                        activebackground="#0f0f0f"),
            "num_guitarNotes" : Spinbox(self.options, values=(1, 2, 4, 8),
                                        bg="#0f0f0f", fg="#5f6363",
                                        activebackground="#0f0f0f"),
            "num_drumNotes" : Spinbox(self.options, values=(1, 2, 4, 8), bg="#0f0f0f", fg="#5f6363",
                                    activebackground="#0f0f0f"),
            "approach_Drum" : Spinbox(self.options, from_=0.0, to=30.0, increment=0.1,
                                        bg="#0f0f0f", fg="#5f6363",
                                        activebackground="#0f0f0f"),
            "approach_Guitar" : Spinbox(self.options, from_=0.0, to=30.0, increment=0.1,
                                        bg="#0f0f0f", fg="#5f6363",
                                        activebackground="#0f0f0f"),
            "allow_long_notes" : Checkbutton(self.options, bg="#0f0f0f", fg="#5f6363",
                                        activebackground="#0f0f0f"),
            "apply": Button(self.options, command=self.apply_editor, text="APPLY")
        }

        self.labelles = {
            #"instr_toggle" : Label(self.options, text="Instrument:", bg="#0f0f0f", fg="#5f6363",
            #                        activebackground="#0f0f0f", activeforeground="#7f8383"),
            "effect_windows" : Label(self.options, text="Windows of Opportunity:", bg="#0f0f0f", fg="#5f6363",
                                    activebackground="#0f0f0f", activeforeground="#7f8383"),
            "bad" : Label(self.options, text="Bad", bg="#0f0f0f", fg="#5f6363",
                                    activebackground="#0f0f0f", activeforeground="#7f8383"),
            "good" : Label(self.options, text="Good", bg="#0f0f0f", fg="#5f6363",
                                    activebackground="#0f0f0f", activeforeground="#7f8383"),
            "excellent" : Label(self.options, text="Excellent", bg="#0f0f0f", fg="#5f6363",
                                    activebackground="#0f0f0f", activeforeground="#7f8383"),
            "num_notes1" : Label(self.options, text="#Guitar Notes", bg="#0f0f0f", fg="#5f6363",
                                    activebackground="#0f0f0f", activeforeground="#7f8383"),
            "num_notes2" : Label(self.options, text="#Drums", bg="#0f0f0f", fg="#5f6363",
                                    activebackground="#0f0f0f", activeforeground="#7f8383"),
            "approach_times" : Label(self.options, text="Approach Times", bg="#0f0f0f", fg="#5f6363",
                                    activebackground="#0f0f0f", activeforeground="#7f8383"),
            "approach_time1" : Label(self.options, text="Guitar", bg="#0f0f0f", fg="#5f6363",
                                    activebackground="#0f0f0f", activeforeground="#7f8383"),
            "approach_time2" : Label(self.options, text="Drum", bg="#0f0f0f", fg="#5f6363",
                                    activebackground="#0f0f0f", activeforeground="#7f8383")
        }

        #self.snootsN_whoosles["instr_toggle1"].place(relx = 0.45, rely=0.03)
        #self.snootsN_whoosles["instr_toggle2"].place(relx = 0.45, rely=0.06)
        self.snootsN_whoosles["bad_window"].place(relx = 0.47, rely=0.14, relwidth=0.3)
        self.snootsN_whoosles["good_window"].place(relx = 0.47, rely=0.17, relwidth=0.3)
        self.snootsN_whoosles["excellent_window"].place(relx = 0.47, rely=0.2, relwidth=0.3)
        self.snootsN_whoosles["num_guitarNotes"].place(relx = 0.47, rely=0.25, relwidth=0.3)
        self.snootsN_whoosles["num_drumNotes"].place(relx = 0.47, rely=0.28, relwidth=0.3)
        self.snootsN_whoosles["approach_Guitar"].place(relx = 0.47, rely=0.36, relwidth=0.3)
        self.snootsN_whoosles["approach_Drum"].place(relx = 0.47, rely=0.39, relwidth=0.3)
        self.snootsN_whoosles["apply"].place(relx = 0.2, rely=0.8, relwidth=0.6)

        #self.labelles["instr_toggle"].place(relx=0.05, rely=0.02)
        self.labelles["effect_windows"].place(relx=0.05, rely=0.1)
        self.labelles["bad"].place(relx=0.05, rely=0.14)
        self.labelles["good"].place(relx=0.05, rely=0.17)
        self.labelles["excellent"].place(relx=0.05, rely=0.2)
        self.labelles["num_notes1"].place(relx=0.05, rely=0.25)
        self.labelles["num_notes2"].place(relx=0.05, rely=0.28)
        self.labelles["approach_times"].place(relx=0.05, rely=0.32)
        self.labelles["approach_time1"].place(relx=0.05, rely=0.36)
        self.labelles["approach_time2"].place(relx=0.05, rely=0.39)

    def apply_editor(self):
        self.info.beat_map_settings["approachTimeDrum"] = self.snootsN_whoosles["approach_Drum"].get()
        self.info.beat_map_settings["approachTimeGuitar"] = self.snootsN_whoosles["approach_Guitar"].get()
        self.info.beat_map_settings["numberOfDrums"] = self.snootsN_whoosles["num_drumNotes"].get()
        self.info.beat_map_settings["numberOfGuitarNotes"] = self.snootsN_whoosles["num_guitarNotes"].get()
        self.info.beat_map_settings["excellentWindow"] = self.snootsN_whoosles["excellent_window"].get()
        self.info.beat_map_settings["goodWindow"] = self.snootsN_whoosles["good_window"].get()
        self.info.beat_map_settings["badWindow"] = self.snootsN_whoosles["bad_window"].get()

    def doAboutMenu(self):
        if self.info.left_mode == "about":
            self.leaveAboutMenu()
            self.emptyMenu()
            self.info.left_mode = "none"
        else:
            if self.info.left_mode == "none":
                self.unemptyMenu()
            elif self.info.left_mode == "edit":
                self.leaveEditMenu()
            elif self.info.left_mode == "file":
                self.leaveFileMenu()
            
            self.enterAboutMenu()
            self.info.left_mode = "about"

    def leaveAboutMenu(self):
        self.about_text.destroy()
        self.working_area.sash_place(0, self.w_w // 3, 0)

    def enterAboutMenu(self):
        self.about_text = Text(self.options, bg="#0f0f0f", fg="#5f6363", width=40, relief=FLAT)
        about_text = ["Music Charter (c)2021 pyrevoid15",
                      "\n\nThis application is meant to facilitate",
                      "\neasy charting for the Rhythm minigame."
                     ]
        self.about_text.insert(INSERT, "")
        for line in about_text:
            self.about_text.insert(END, line)
        self.about_text.place(relx=0.1, rely=0.3)
        self.working_area.sash_place(0, self.w_w // 2, 0)


class Main:
    def __init__(self):
        pass


if __name__ == "__main__":
    gui = Tk()
    app = Window(gui)
    app.mainloop()
